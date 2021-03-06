using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;
using STARK_Project.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using STARK_Project.DatabaseModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using STARK_Project.NotificationServices;
using Microsoft.AspNetCore.Authorization;

namespace STARK_Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;
        private readonly INotificationService _notificationService;
        private readonly string _userId;
        public DetailsController(IHttpContextAccessor httpContextAccessor, ICryptoService service, IDbService dbService, INotificationService notificationService)
        {
            _service = service;
            _dbService = dbService;
            _notificationService = notificationService;
            _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        public IActionResult Index(string cryptocurrency = "BTC", string currency = "USD")
        {
            var data = new DetailsViewModel();
            data.CryptoModel = _service.GetCryptocurrencyInfoAsync(cryptocurrency, currency).Result;
            data.Cryptocurrencies = _service.GetCryptocurrenciesAsync().Result;
            data.Currencies = _service.GetCurrencies();
            data.HistoricalData = _service.GetHistoricalData(HistoricalDataTypes.Daily, cryptocurrency, currency, 30, 1).Result;

            List<DataPoint> dataPoints = new List<DataPoint>();
            // data for the chart
            foreach (var record in data.HistoricalData.Data)
            {
                dataPoints.Add(new DataPoint(record.Time * 1000, new double[] { record.Open, record.High, record.Low, record.Close }));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            ViewBag.ConditionTypes = new List<SelectListItem> {
                new SelectListItem("Wartość", "value")};

            ViewBag.ConditionRelativities = new List<SelectListItem>
            {
                new SelectListItem("Kiedy spadnie poniżej", "drop below"),
                new SelectListItem("Kiedy wzrośnie powyżej", "rise above")
            };

            if (_userId != null)
            {
                var currentConditions = _dbService.GetConditions(_userId);
                currentConditions = currentConditions.Where(x => x.Cryptocurrency.Symbol == cryptocurrency).ToList();
                if (currentConditions != null)
                {
                    ViewBag.CurrencyConditions = currentConditions;
                }

                ViewBag.IsSubscribed = _dbService.GetWatchlist(_userId).Result.Where(x => x.Symbol == cryptocurrency).Count() > 0 ? true : false;
            }
            else
            {
                ViewBag.CurrencyConditions = new List<Condition>();

                ViewBag.IsSubscribed = false;
            }
            return View(data);
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        [Authorize]
        public async Task<IActionResult> AddToWatchList(string cryptocurrency = "BTC", string currency = "USD")
        {
            if (_userId != null)
            {
                await _dbService.AddToWatchListAsync(_userId, cryptocurrency);
                return RedirectToAction("Index", new { cryptocurrency = cryptocurrency, currency = currency });
            }
            return RedirectToAction("Index", new { cryptocurrency = cryptocurrency, currency = currency });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNotification(string symbol, string type, string relative, string value, string currentCurrency)
        {
            // TODO: może przydać się walidacja

            if (_userId != null)
            {
                var condition = new Condition();
                var notifMessage = "";
                if (type == "value")
                {
                    if (relative == "drop below")
                    {
                        condition.TresholdMin = float.Parse(value);
                        notifMessage = $"The price of {symbol} has dropped below {value}.";
                    }
                    else
                    {
                        condition.TresholdMax = float.Parse(value);
                        notifMessage = $"The price of {symbol} has risen above {value}.";
                    }
                }
               condition = await _dbService.AddConditionAsync(_userId, symbol, condition);

                // added for notification
                _notificationService.CreateConditionNotifyTresholdMax(_userId, condition);

            }
            return View("Index", new { cryptocurrency = symbol, currency = currentCurrency });
        }

        public async Task<IActionResult> RemoveNotification(int conditionId, string currentCrypto, string currentCurrency)
        {
            if (_userId != null)
            {
                await _dbService.RemoveConditionAsync(_userId, conditionId);
            }
            return RedirectToAction("Index", new { cryptocurrency = currentCrypto, currency = currentCurrency });
        }

        [HttpGet]
        public IActionResult GetCurrencyConditions(string currencySymbol)
        {
            var currencyConditions = _dbService.GetConditions(_userId).Where(x => x.Cryptocurrency.Symbol == currencySymbol).ToList();

            if (currencySymbol.Count() > 0)
            {
                return Json(currencySymbol);
            }

            return Json(new List<Condition>());
        }
    }

    [DataContract]
    public class DataPoint
    {
        [DataMember(Name = "x")]
        public double X { get; set; }
        [DataMember(Name = "y")]
        public double[] Y { get; set; }
        public DataPoint(double x, double[] y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
