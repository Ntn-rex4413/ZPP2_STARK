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

        public async Task<bool> AddCryptocurrencyToDatabaseAsync(Cryptocurreny cryptocurreny)
        {
            try
            {
                if (_context.Cryptocurrenies.Count(x => x.Name.Equals(cryptocurreny.Name)) > 0) return false;

                await _context.Cryptocurrenies.AddAsync(cryptocurreny);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddToWatchListAsync(User user, Cryptocurreny cryptocurreny)
        {
            try
            {
                var userToUpdate = await GetUser(user);

                if (userToUpdate.Watchlist.Count(x => x.Symbol.Equals(cryptocurreny.Symbol)) > 0) return false;
                userToUpdate.Watchlist.Add(cryptocurreny);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ClearWatchlist(User user)
        {
            try
            {
                var toClear = await GetUser(user);
                if (toClear is null) return false;
                else toClear.Watchlist.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ICollection<Cryptocurreny>> GetWatchlist(User user)
        {
            try
            {
                var result = await GetUser(user);
                if (result is null) return null;
                else return result.Watchlist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RemoveFromWatchListAsync(User user, Cryptocurreny cryptocurreny)
        {
            try
            {
                var userToUpdate = await GetUser(user);

                var coinToDelete = await _context.Cryptocurrenies.FirstOrDefaultAsync(x => x.Symbol.Equals(cryptocurreny.Symbol));
                 return   userToUpdate.Watchlist.Remove(coinToDelete);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Task<User> GetUser(User user)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(user.Email))
        }
    }
}
