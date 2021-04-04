using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index(string currencyName)
        {
            // temp code - to be updated
            IEnumerable<CurrencyDummy> availableCurrencies = new List<CurrencyDummy>
            {
                new CurrencyDummy{Name="Bitcoin", UnitPrice= 42360.27, ChangePercentage=-2, ChangePrice=2000, LastUpdated=DateTime.Now},
                new CurrencyDummy{Name="Ethereum", UnitPrice = 1234.56, ChangePercentage=4, ChangePrice=1200, LastUpdated=DateTime.Now}
            };

            CurrencyDummy currency = availableCurrencies.Where(x => x.Name == currencyName).First();

            return View(currency);
        }
    }
}
