using Microsoft.EntityFrameworkCore;
using STARK_Project.Data;
using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
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

        public async Task<bool> AddCryptocurrenciesToDatabaseAsync(ICollection<Cryptocurreny> cryptocurrencies)
        {
            try
            {
                var coinList = _context.Cryptocurrenies.ToHashSet(new CoinHash());
                
                var toUpdate = new List<Cryptocurreny>();
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

        public async Task<bool> AddCryptocurrencyToDatabaseAsync(Cryptocurreny cryptocurreny)
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
            try
            {
                var userToUpdate = await GetUser(userId);
                var coinToAdd = await _context.Cryptocurrenies.FirstOrDefaultAsync(x => x.Symbol == "Cryptocurrency");
                if (userToUpdate.Watchlist.Count(x => x.Symbol.Equals(coinToAdd)) > 0) return false;
                userToUpdate.Watchlist.Add(coinToAdd);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ClearWatchlist(string userId)
        {
            try
            {
                var toClear = await GetUser(userId);
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

        public async Task<ICollection<Cryptocurreny>> GetWatchlist(string userId)
        {
            try
            {
                var result = await GetUser(userId);
                if (result is null) return null;
                else return result.Watchlist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RemoveFromWatchListAsync(string userId, Cryptocurreny cryptocurreny)
        {
            try
            {
                var userToUpdate = await GetUser(userId);

                var coinToDelete = await _context.Cryptocurrenies.FirstOrDefaultAsync(x => x.Symbol.Equals(cryptocurreny.Symbol));
                 var result =  userToUpdate.Watchlist.Remove(coinToDelete);
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

        private Task<User> GetUser(string userId)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
        }
    }
}
