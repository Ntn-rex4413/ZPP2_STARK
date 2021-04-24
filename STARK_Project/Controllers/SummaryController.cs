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
using System.Diagnostics;

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

            return View(data);
        }
    }
}
