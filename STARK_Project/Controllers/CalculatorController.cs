using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;

        private readonly string _userId;

        public CalculatorController(IHttpContextAccessor httpContextAccessor, ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;
            _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public IActionResult Index(double valueLeft = 0, double valueRight = 0, string currencyLeft = "EUR", string currencyRight = "BTC")
        {
            CalculatorViewModel viewModel = new CalculatorViewModel();

            viewModel.LeftCurrency = currencyLeft;
            viewModel.RightCurrency = currencyRight;
            viewModel.LeftValue = valueLeft;
            viewModel.RightValue = valueRight;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(double valueLeft, string currencyLeft, string currencyRight)
        {
            //TODO
            //calculate new value
            //return view model
            return View();
        }
    }
}
