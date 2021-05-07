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

namespace STARK_Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ICryptoService _service;
        private readonly IDbService _dbService;
        private readonly string _userId;
        public DetailsController(IHttpContextAccessor httpContextAccessor, ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;
            _userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

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

            // dummy data for testing display
            //ViewBag.CurrencyConditions = new List<Condition> { 
            //    new Condition { TresholdMax = 500, Cryptocurrency = new Cryptocurrency { Name = "Bitcoin", Symbol = "BTC" } },
            //    new Condition { TresholdMin = 600, Cryptocurrency = new Cryptocurrency { Name = "Ethereum", Symbol = "ETH" } },
            //};
            // data for conditions list

            ViewBag.ConditionTypes = new List<SelectListItem> {
                new SelectListItem("Procent", "percentage"),
                new SelectListItem("Wartość", "value")};

            var currentConditions = _dbService.GetConditions(_userId).Where(x => x.Cryptocurrency.Symbol == cryptocurrency).ToList();
            if (currentConditions != null)
            {
                ViewBag.CurrencyConditions = currentConditions;
            }
            else
            {
                ViewBag.CurrencyConditions = new List<Condition>();
            }
            return View(data);
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

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
        public async Task<IActionResult> AddNotification(string symbol, string type, string relative, string value)
        {
            var condition = new Condition();
            await _dbService.AddConditionAsync(_userId, symbol, condition);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveNotification(int conditionId)
        {
            await _dbService.RemoveConditionAsync(_userId, conditionId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencyConditions(string currencySymbol)
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
