using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using STARK_Project.CryptoAPIService;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ICryptoService _service;

        private readonly IDbService _dbService;
        public SummaryController(ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;
        }
        public IActionResult Index(string currency = "PLN")
        {
            var data = new SummaryViewModel();
            data.CryptoModel = _service.GetCryptocurrenciesInfoAsync(currency).Result;
            data.Cryptocurrencies = _service.GetCryptocurrenciesAsync().Result;
            data.Currencies = _service.GetCurrencies();

            //temp
            //add new cryptocurrencies
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 0,
                Name = "Bitcoin",
                Symbol = "BTC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 1,
                Name = "Litecoin",
                Symbol = "LTC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 2,
                Name = "Namecoin",
                Symbol = "NMC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 3,
                Name = "Peercoin",
                Symbol = "PPC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 4,
                Name = "Dogecoin",
                Symbol = "DOGE",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 5,
                Name = "Gridcoin",
                Symbol = "GRC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 6,
                Name = "Primecoin",
                Symbol = "XPM",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 7,
                Name = "Ripple",
                Symbol = "XRP",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 8,
                Name = "Nxt",
                Symbol = "NXT",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 9,
                Name = "Auracoin",
                Symbol = "AUR",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 10,
                Name = "Dash",
                Symbol = "DASH",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 11,
                Name = "NEO",
                Symbol = "NEO",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 12,
                Name = "MazaCoin",
                Symbol = "MZC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 13,
                Name = "Monero",
                Symbol = "XMR",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 14,
                Name = "Titcoin",
                Symbol = "TIT",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 15,
                Name = "Verge",
                Symbol = "XVG",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 16,
                Name = "Stellar",
                Symbol = "XLM",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 17,
                Name = "Vertcoin",
                Symbol = "VTC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 18,
                Name = "Ethereum",
                Symbol = "ETC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 19,
                Name = "Ethereum Classic",
                Symbol = "ETC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 20,
                Name = "Nano",
                Symbol = "Nano",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 21,
                Name = "Tether",
                Symbol = "USDT",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 22,
                Name = "Zcash",
                Symbol = "ZEC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 23,
                Name = "Bitcoin Cash",
                Symbol = "BCH",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 24,
                Name = "EOS.IO",
                Symbol = "EOS",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Id = 25,
                Name = "Cardano",
                Symbol = "ADA",
            });


            return View(data);
        }
    }
}
