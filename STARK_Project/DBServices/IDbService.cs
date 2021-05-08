using STARK_Project.DatabaseModel;
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
        Task<bool> RemoveFromWatchListAsync(string userId, string cryptocurreny);
        Task<bool> ClearWatchlist(string userId);
        Task<ICollection<Cryptocurrency>> GetWatchlist(string userId);

        Task<bool> AddConditionAsync(string userId, string symbol, Condition condition);
        Task<bool> RemoveConditionAsync(string userId, int conditionId);
        ICollection<Condition> GetConditions(string userId);

        ICollection<Notification> GetNotifications(string userId);
        Task<bool> AddNotification(string userId, string message);
        Task<bool> RemoveNotification(string userId, string message);

    }
}
