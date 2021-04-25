using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DBServices
{
    public interface IDbService
    {
        Task<bool> AddCryptocurrencyToDatabaseAsync(Cryptocurreny cryptocurreny);
        Task<bool> AddCryptocurrenciesToDatabaseAsync(ICollection<Cryptocurreny> cryptocurreny);
        Task<bool> AddToWatchListAsync(string userId, string cryptocurreny);
        Task<bool> RemoveFromWatchListAsync(string userId, Cryptocurreny cryptocurreny);
        Task<bool> ClearWatchlist(string userId);
        Task<ICollection<Cryptocurreny>> GetWatchlist(string userId);
    }
}
