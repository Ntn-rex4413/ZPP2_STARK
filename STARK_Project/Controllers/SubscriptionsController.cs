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
