using Microsoft.AspNetCore.Mvc;
using STARK_Project.CryptoAPIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Logging;
using STARK_Project.CryptoAPIService;
using STARK_Project.Data;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using System.Diagnostics;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;

        private readonly string _userId;

        public SubscriptionsController(IHttpContextAccessor httpContextAccessor, ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;
            _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<IActionResult> Index(string currency = "USD")
        {
            if (_userId == null)
            {
                return RedirectToAction("Index", "Summary");
            }
            else
            {
                var data = new SubscriptionsViewModel();
                // Line below is actual code on main:
                //data.WatchedCryptocurrencies = _dbService.GetWatchlist(_userId).Result.ToList();

                // Dummy data - remove before merge:
                //List<Cryptocurrency> userCurrencies = new List<Cryptocurrency> {
                //new Cryptocurrency{ Id = 1, Name = "Bitcoin", Symbol = "BTC" },
                //new Cryptocurrency{ Id = 2, Name = "DogeCoin", Symbol = "DOGE" }
                //};
                List<Cryptocurrency> userCurrencies = (await _dbService.GetWatchlist(_userId)).ToList();
                data.WatchedCryptocurrencies = new List<SubscribedCryptoViewModel>();
                foreach (var userCurrency in userCurrencies)
                {
                    data.WatchedCryptocurrencies.Add(new SubscribedCryptoViewModel(userCurrency,
                        _service.GetCryptocurrencyInfoAsync(userCurrency.Symbol, currency).Result));
                }

                data.Cryptocurrencies = _service.GetCryptocurrenciesAsync().Result;
                data.Currencies = _service.GetCurrencies();
                return View(data);
            }
        }

    }
}
