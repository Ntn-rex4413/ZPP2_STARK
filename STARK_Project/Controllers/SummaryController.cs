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
        public IActionResult Index()
        {
            List<CryptoModel> data = new List<CryptoModel>();
            var dataPLN = _service.GetCryptocurrenciesInfoAsync(CurrencySymbols.PLN).Result;
            var dataEUR = _service.GetCryptocurrenciesInfoAsync(CurrencySymbols.EUR).Result;
            var dataUSD = _service.GetCryptocurrenciesInfoAsync(CurrencySymbols.USD).Result;

            data.Add(dataPLN);
            data.Add(dataEUR);
            data.Add(dataUSD);

            return View(data);
        }
    }
}
