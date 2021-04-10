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

        public IActionResult Index(string cryptocurrency = "BTC", string currency = "PLN")
        {
            var data = new DetailsViewModel();
            data.CryptoModel = _service.GetCryptocurrencyInfoAsync(cryptocurrency, currency).Result;
            data.Cryptocurrencies = _service.GetCryptocurrencies();
            data.Currencies = _service.GetCurrencies();

            return View(data);
        }
    }
}
