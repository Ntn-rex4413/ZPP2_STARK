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
        private ICryptoService _service;
        public SummaryController(ICryptoService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
