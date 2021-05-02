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
            data.HistoricalData = _service.GetHistoricalData(HistoricalDataTypes.Daily, cryptocurrency, currency, 7, 7).Result;

            List<DataPoint> dataPoints = new List<DataPoint>();
            // data for the chart
            foreach (var record in data.HistoricalData.Data)
            {
                dataPoints.Add(new DataPoint(UnixTimeStampToDateTime(record.Time), new double[] { record.Open, record.High, record.Low, record.Close }));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

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
    }

    [DataContract]
    public class DataPoint
    {
        [DataMember(Name = "x")]
        public Nullable<DateTime> X { get; set; }
        [DataMember(Name = "y")]
        public double[] Y { get; set; }
        public DataPoint(DateTime x, double[] y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
