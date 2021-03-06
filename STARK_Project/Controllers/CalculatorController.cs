using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;
using STARK_Project.Models;
using STARK_Project.Calculator;

namespace STARK_Project.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;

        private readonly ICalculator _calculator;

        private readonly string _userId;

        public CalculatorController(IHttpContextAccessor httpContextAccessor, ICryptoService service, IDbService dbService, ICalculator calculator)
        {
            _service = service;
            _dbService = dbService;
            _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _calculator = calculator;
        }

        [HttpGet]
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
        public IActionResult Index(CalculatorViewModel viewModel)
        {
            //CalculatorConverter calculator = new CalculatorConverter(_dbService, _service);
            viewModel.RightValue = _calculator.Calculate(viewModel.LeftValue, viewModel.LeftCurrency, viewModel.RightCurrency);
            return RedirectToAction("Index", new {valueLeft = viewModel.LeftValue, valueRight = viewModel.RightValue, currencyLeft = viewModel.LeftCurrency, currencyRight = viewModel.RightCurrency});
        }
    }
}
