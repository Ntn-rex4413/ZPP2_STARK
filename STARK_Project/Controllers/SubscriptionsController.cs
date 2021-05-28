using Microsoft.AspNetCore.Mvc;
using STARK_Project.CryptoAPIService;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using STARK_Project.Models;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string currency = "USD")
        {
            if (false)
            {
                //TODO
                //remove at the very end
                var coins = await _service.GetCryptocurrenciesAsync();
                await _dbService.AddCryptocurrenciesToDatabaseAsync(coins.Select(x => new Cryptocurrency { Symbol = x.Key, Name = x.Value }).ToList());
            }
            if (_userId == null)
            {
                return RedirectToAction("Index", "Summary");
            }
            else
            {
                var data = new SubscriptionsViewModel();
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

        public async Task<IActionResult> RemoveFromWatchList(string cryptocurrency = "BTC", string currency = "USD")
        {
            if (_userId != null)
            {
                await _dbService.RemoveFromWatchListAsync(_userId, cryptocurrency);
                return RedirectToAction("Index", new { currency = currency });
            }
            return RedirectToAction("Index", new { currency = currency });
        }

        [Authorize]
        public IActionResult Alerts()
        {
            if (_userId != null)
            {
                var userConditions = _dbService.GetConditions(_userId);

                return View(userConditions);
            }
            return RedirectToAction("Index", "Summary", new { currency = "PLN" });
        }

    }
}
