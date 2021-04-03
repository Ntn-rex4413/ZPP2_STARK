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
            STARK_Project.Controllers.CurrencyDummy currency = new CurrencyDummy { Name = currencyName };
            return View(currency);
        }
    }
}
