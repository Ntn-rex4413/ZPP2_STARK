using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DBServices
{
    interface IDbService
    {
        Task<bool> AddCryptocurrencyToDatabaseAsync(Cryptocurreny cryptocurreny);
        Task<bool> AddToWatchListAsync(User user, Cryptocurreny cryptocurreny);
        Task<bool> RemoveFromWatchListAsync(User user, Cryptocurreny cryptocurreny);
    }
}
