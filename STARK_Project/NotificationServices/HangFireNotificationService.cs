using STARK_Project.CryptoAPIService;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using System;
using Hangfire;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.NotificationServices
{
    public class HangFireNotificationService : INotificationService
    {
        private IDbService _dbService;
        private ICryptoService _cyptoService;
        private static readonly string _currency = "PLN";
       
        public HangFireNotificationService(IDbService dbService, ICryptoService cyptoService)
        {
            _dbService = dbService;
            _cyptoService = cyptoService;
        }

        public void CreateConditionNotify(string userId, Condition condition)
        {
            var jobId = GetUniqueJobId(userId);
            RecurringJob.AddOrUpdate(jobId, () => IsMaxTresholdExceeded(userId, condition, jobId), Cron.Minutely);
            RecurringJob.Trigger(jobId);
        }

        private async void IsMaxTresholdExceeded(string userId, Condition condition,string jobId)
        {
            
            var coinInfo = await _cyptoService.GetCryptocurrencyInfoAsync(condition.Cryptocurrency.Symbol, _currency);
            if(coinInfo.ChangeHour >= condition.TresholdMax)
            {
               await _dbService.AddNotification(userId, TresholdMaxMesssage(condition));
                RecurringJob.RemoveIfExists(jobId);
            }
        }
        private string GetUniqueJobId(string userId)
        {
            return userId + Guid.NewGuid();
        }
        private string TresholdMaxMesssage(Condition condition)
        {
            return $"Treshold max: {condition.TresholdMax} has benn exceeded in {condition.Cryptocurrency.Name} cryptocurrency!";
        }
    }
}
