﻿using Microsoft.AspNetCore.Mvc;
using System;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using STARK_Project.CryptoAPIService;

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
            var data = _service.GetCryptocurrencyInfoAsync(cryptocurrency, currency).Result;
            return View(data);
        }
    }
}
