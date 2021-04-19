using STARK_Project.Data;
using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<bool> AddToWatchListAsync(User user, Cryptocurreny cryptocurreny)
        {
            throw new NotImplementedException();
        }
    }
}
