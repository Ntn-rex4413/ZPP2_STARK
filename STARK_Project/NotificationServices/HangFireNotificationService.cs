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
        private readonly IRecurringJobManager _manager;

        private static readonly string _currency = "USD";
  
       
        //public HangFireNotificationService(IDbService dbService, ICryptoService cyptoService)
        //{
        //    _dbService = dbService;
        //    _cyptoService = cyptoService;
        //}

        public HangFireNotificationService(IDbService dbService, ICryptoService cyptoService, IRecurringJobManager manager)
        {
            _dbService = dbService;
            _cyptoService = cyptoService;
            _manager = manager;

        }

        public void CreateConditionNotifyTresholdMax(string userId, Condition condition)
        {
            var jobId = GetUniqueJobId(userId);
            _manager.AddOrUpdate(jobId, ()=> IsMaxTresholdExceeded(userId, 
                condition.TresholdMax, 
                condition.Cryptocurrency.Symbol, 
                jobId), 
                "* * * * *");
            //RecurringJob.Trigger(jobId);
        }

        public void TestMethod(int a, Condition c)
        {
            //var con = (Condition)c;
            Console.WriteLine($"write line {a} {c.TresholdMax}");
        }
        public async Task IsMaxTresholdExceeded(string userId, double value, string symbol, string jobId)
        {
            var coinInfo = await _cyptoService.GetCryptocurrencyInfoAsync(symbol, _currency);
            Console.WriteLine($"coin info {coinInfo}");
            if (coinInfo.Price >= value)
            {
                Console.WriteLine("Spelniono warunek!");
                await _dbService.AddNotification(userId, TresholdMaxMesssage(value, symbol));
                //RecurringJob.RemoveIfExists(jobId);
            }
        }


        private string GetUniqueJobId(string userId)
        {
            return userId + Guid.NewGuid();
        }
        private string TresholdMaxMesssage(double value, string symbol)
        {
            return $"Treshold max: {value} has benn exceeded in {symbol} cryptocurrency!";
        }
    }
}
