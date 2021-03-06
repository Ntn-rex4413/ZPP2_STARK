using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using STARK_Project.CryptoAPIService;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using STARK_Project.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace STARK_Project.Controllers
{
    public class SummaryController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;

        private readonly string _userId;

        public SummaryController(IHttpContextAccessor httpContextAccessor, ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;
            _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        public IActionResult Index(string currency = "PLN")
        {
            var data = new SummaryViewModel();
            data.CryptoModel = _service.GetCryptocurrenciesInfoAsync(currency).Result;
            data.Cryptocurrencies = _service.GetRankingDataAsync(10, currency).Result;
            data.Currencies = _service.GetCurrencies();

            ViewBag.IsUserLoggedIn = _userId != null;

            return View(data);
        }

        [Authorize]
        public async Task<IActionResult> AddToWatchList(string cryptocurrency)
        {
            if (_userId != null)
            {
                await _dbService.AddToWatchListAsync(_userId, cryptocurrency);
                return RedirectToAction("Index", new {currency = "PLN"});
            }
            return RedirectToAction("Index", new { currency = "PLN" });
        }

        public IActionResult Search(string cryptocurrency, string currency = "PLN")
        {
            var data = new SummaryViewModel();
            data.CryptoModel = _service.GetCryptocurrenciesInfoAsync(currency).Result;

            if (!String.IsNullOrEmpty(cryptocurrency))
            {
                data.Cryptocurrencies = _dbService.GetMatchingCryptoNames(cryptocurrency).Result;
            }
            else
            {
                data.Cryptocurrencies = _service.GetRankingDataAsync(10, currency).Result;
            }
            data.Currencies = _service.GetCurrencies();
            ViewBag.IsUserLoggedIn = _userId != null;

            return View("Index", data);
        }
    }
}
