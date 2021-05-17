using STARK_Project.CryptoAPIService;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using System;
using Hangfire;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.EmailServices;

namespace STARK_Project.NotificationServices
{
    public class HangFireNotificationService : INotificationService
    {
        private IDbService _dbService;
        private ICryptoService _cyptoService;
        private readonly IRecurringJobManager _manager;
        private readonly IEmailService _emailService;
        private readonly string _email = "START@project.com";


        private static readonly string _currency = "USD";


        //public HangFireNotificationService(IDbService dbService, ICryptoService cyptoService)
        //{
        //    _dbService = dbService;
        //    _cyptoService = cyptoService;
        //}



        public HangFireNotificationService(IDbService dbService, ICryptoService cyptoService, IRecurringJobManager manager, IEmailService emailService)
        {
            _dbService = dbService;
            _cyptoService = cyptoService;
            _manager = manager;
            _emailService = emailService;
        }

        public void CreateConditionNotifyTresholdMax(string userId, Condition condition)
        {
            var jobId = GetUniqueJobId(userId);
            _manager.AddOrUpdate(jobId, () => IsMaxTresholdExceeded(userId,
                condition.TresholdMax,
                condition.Cryptocurrency.Symbol,
                jobId),
                "* * * * *");

        }

        public async Task IsMaxTresholdExceeded(string userId, double value, string symbol, string jobId)
        {
            var coinInfo = await _cyptoService.GetCryptocurrencyInfoAsync(symbol, _currency);
            Console.WriteLine($"coin info {coinInfo}");
            if (coinInfo.Price >= value)
            {
                var message = TresholdMaxMesssage(value, symbol);
                var userEmail = _dbService.GetUserEmail(userId);
                await _emailService.SendEmail(_email, userEmail, "Max treshold was exceeded!", message);

                Console.WriteLine("Spelniono warunek!");
                await _dbService.AddNotification(userId, message);
                RecurringJob.RemoveIfExists(jobId);
            }
        }

        public void CreateConditionNotifyTresholdMin(string userId, Condition condition)
        {
            var jobId = GetUniqueJobId(userId);
            _manager.AddOrUpdate(jobId, () => IsMinTresholdExceeded(userId,
                condition.TresholdMax,
                condition.Cryptocurrency.Symbol,
                jobId),
                "* * * * *");

        }

        public async Task IsMinTresholdExceeded(string userId, double value, string symbol, string jobId)
        {
            var coinInfo = await _cyptoService.GetCryptocurrencyInfoAsync(symbol, _currency);
            Console.WriteLine($"coin info {coinInfo}");
            if (coinInfo.Price <= value)
            {
                var message = TresholdMinMesssage(value, symbol);
                var userEmail = _dbService.GetUserEmail(userId);
                await _emailService.SendEmail(_email, userEmail, "Min treshold was exceeded!", message);

                Console.WriteLine("Spelniono warunek!");
                await _dbService.AddNotification(userId, message);
                RecurringJob.RemoveIfExists(jobId);
            }
        }

        private string TresholdMinMesssage(double value, string symbol)
        {
            return $"Treshold min: {value} has benn exceeded in {symbol} cryptocurrency!";
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
