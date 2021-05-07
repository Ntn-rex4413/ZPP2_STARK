using Microsoft.EntityFrameworkCore;
using STARK_Project.Data;
using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace STARK_Project.DBServices
{
    public class ApplicationDbService : IDbService
    {
        private ApplicationDbContext _context;

        public ApplicationDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Condition> GetConditions(string userId)
        {
            return GetUser(userId).Conditions;
        }

        public async Task<bool> AddConditionAsync(string userId, string symbol, Condition condition)
        {
            var user = GetUser(userId);
            if (user is null) return false;

            var cyptoSymbol = await _context.Cryptocurrenies.FirstOrDefaultAsync(x => x.Symbol == symbol);
            if (cyptoSymbol is null) return false;

            condition.Cryptocurrency = cyptoSymbol;
            condition.User = user;
            await _context.Conditions.AddAsync(condition);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveConditionAsync(string userId, int conditionId)
        {
            var user = GetUser(userId);
            if (user is null) return false;

            var condition = user.Conditions.FirstOrDefault(x => x.Id == conditionId);
            if (condition is null) return false;
            user.Conditions.Remove(condition);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddCryptocurrenciesToDatabaseAsync(ICollection<Cryptocurrency> cryptocurrencies)
        {
            try
            {
                var coinList = _context.Cryptocurrenies.ToHashSet(new CoinHash());
                
                var toUpdate = new List<Cryptocurrency>();
                foreach (var coin in cryptocurrencies)
                    if (!coinList.Contains(coin)) toUpdate.Add(coin);
                if (toUpdate.Count == 0) return false;
                await _context.Cryptocurrenies.AddRangeAsync(toUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddCryptocurrencyToDatabaseAsync(Cryptocurrency cryptocurreny)
        {
            try
            {
                if (_context.Cryptocurrenies.Count(x => x.Name.Equals(cryptocurreny.Name)) > 0) return false;

                await _context.Cryptocurrenies.AddAsync(cryptocurreny);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddToWatchListAsync(string userId, string cryptocurreny)
        {
            //try
            //{
                var userToUpdate =  GetUser(userId);
            Debug.WriteLine("User: " + userToUpdate.Id);
            Debug.WriteLine(userToUpdate.Watchlist);
            if (userToUpdate.Watchlist is null) userToUpdate.Watchlist = new List<Cryptocurrency>();
                var coinToAdd =  _context.Cryptocurrenies.FirstOrDefault(x => x.Symbol == cryptocurreny);
                if (userToUpdate.Watchlist.Count(x => x.Symbol.Equals(coinToAdd)) > 0) return false;
                userToUpdate.Watchlist.Add(coinToAdd);
                _context.SaveChanges();
                return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        public async Task<bool> ClearWatchlist(string userId)
        {
            try
            {
                var toClear =  GetUser(userId);
                if (toClear is null) return false;
                else
                {
                    toClear.Watchlist.Clear();
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ICollection<Cryptocurrency>> GetWatchlist(string userId)
        {
            try
            {
                var result =  GetUser(userId);
                if (result is null) return null;
                else return _context.Users.Include(x=> x.Watchlist).FirstOrDefault(x => x.Id == userId).Watchlist;
            }
            catch (Exception)
            {
                return null;
            }
        }

       

        public async Task<bool> RemoveFromWatchListAsync(string userId, string cryptocurreny)
        {
            try
            {
                var userToUpdate =  GetUser(userId);

                var coinToDelete = await _context.Cryptocurrenies.FirstOrDefaultAsync(x => x.Symbol.Equals(cryptocurreny));
                //var result =  userToUpdate.Watchlist.Remove(coinToDelete);
                var result = GetWatchlist(userId).Result.Remove(coinToDelete);
                if (result)
                {
                    _context.SaveChanges();
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private  User GetUser(string userId)
        {
            return  _context.Users.FirstOrDefault(x => x.Id == userId);
        }

    
    }
}
