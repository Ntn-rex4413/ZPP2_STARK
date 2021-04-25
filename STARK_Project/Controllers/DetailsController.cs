using Microsoft.AspNetCore.Mvc;
using System;

using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;
        public DetailsController(ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;
        }

        public IActionResult Index(string cryptocurrency = "BTC", string currency = "USD")
        {
            var data = new DetailsViewModel();
            data.CryptoModel = _service.GetCryptocurrencyInfoAsync(cryptocurrency, currency).Result;
            data.Cryptocurrencies = _service.GetCryptocurrenciesAsync().Result;
            data.Currencies = _service.GetCurrencies();

            return View(data);
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
