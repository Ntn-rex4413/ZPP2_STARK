using Microsoft.AspNetCore.Mvc;
using System;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using STARK_Project.CryptoAPIService;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ICryptoService _service;
        public DetailsController(ICryptoService service)
        {
            _service = service;
        }

        public IActionResult Index(string cryptocurrency = "BTC", string currency = "USD")
        {
            var data = new DetailsViewModel();
            data.CryptoModel = _service.GetCryptocurrencyInfoAsync(cryptocurrency, currency).Result;
            data.Cryptocurrencies = _service.GetCryptocurrencies();
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
