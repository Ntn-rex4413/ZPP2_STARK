using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoApiModel;
using STARK_Project.CryptoApiModel.CurrencyEnums;
using STARK_Project.CryptoApiModel.CurrencySymbolsEnums;
using STARK_Project.CryptoAPIService;

namespace STARK_Project.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ICryptoService _service;
        public SummaryController(ICryptoService service)
        {
            _service = service;
        }
        public IActionResult Index(string currency = null)
        {
            currency ??= "PLN"; //can change to global default later

            var data = _service.GetCryptocurrenciesInfoAsync(Enum.Parse<CurrencySymbols>(currency)).Result;

            return View(data);
        }
    }
}
