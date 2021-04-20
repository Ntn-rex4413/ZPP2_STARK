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
                Name = "Bitcoin",
                Symbol = "BTC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Litecoin",
                Symbol = "LTC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Namecoin",
                Symbol = "NMC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Peercoin",
                Symbol = "PPC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Dogecoin",
                Symbol = "DOGE",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Gridcoin",
                Symbol = "GRC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Primecoin",
                Symbol = "XPM",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Ripple",
                Symbol = "XRP",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Nxt",
                Symbol = "NXT",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Auracoin",
                Symbol = "AUR",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Dash",
                Symbol = "DASH",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "NEO",
                Symbol = "NEO",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "MazaCoin",
                Symbol = "MZC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Monero",
                Symbol = "XMR",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Titcoin",
                Symbol = "TIT",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Verge",
                Symbol = "XVG",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Stellar",
                Symbol = "XLM",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Vertcoin",
                Symbol = "VTC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Ethereum",
                Symbol = "ETC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Ethereum Classic",
                Symbol = "ETC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Nano",
                Symbol = "Nano",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Tether",
                Symbol = "USDT",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Zcash",
                Symbol = "ZEC",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Bitcoin Cash",
                Symbol = "BCH",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "EOS.IO",
                Symbol = "EOS",
            });
            _dbService.AddCryptocurrencyToDatabaseAsync(new Cryptocurreny
            {
                Name = "Cardano",
                Symbol = "ADA",
            });

             
            return View(data);
        }
    }
}
