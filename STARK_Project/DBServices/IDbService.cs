﻿using STARK_Project.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.DBServices
{
    public interface IDbService
    {
        Task<bool> AddCryptocurrencyToDatabaseAsync(Cryptocurrency cryptocurreny);
        Task<bool> AddCryptocurrenciesToDatabaseAsync(ICollection<Cryptocurrency> cryptocurreny);
        Task<bool> AddToWatchListAsync(string userId, string cryptocurreny);
        Task<bool> RemoveFromWatchListAsync(string userId, Cryptocurrency cryptocurreny);
        Task<bool> AddCondtionAsync(string userId, string symbol, Condition condition);
        Task<bool> RemoveCondtionAsync(string userId, Condition condition);
        Task<bool> ClearWatchlist(string userId);
        Task<ICollection<Cryptocurrency>> GetWatchlist(string userId);
    }
}
