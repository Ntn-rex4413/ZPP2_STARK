using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Controllers
{
    public class SummaryController : Controller
    {
        public IActionResult Index()
        {
            // temp code - remove before merge
            IEnumerable<CurrencyDummy> dummyCurrencies = new List<CurrencyDummy>
            {
                new CurrencyDummy{Name="Bitcoin"},
                new CurrencyDummy{Name="Ethereum"}
            };

            return View(dummyCurrencies);
        }
    }

    // temp code - remove before merge
    public class CurrencyDummy
    {
        public string Name { get; set; }
    }
}
