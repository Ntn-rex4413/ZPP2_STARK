using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index(double valueLeft = 0, double valueRight = 0, string currencyLeft = "EUR", string currencyRight = "BTC")
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(double valueLeft, string currencyLeft, string currencyRight)
        {
            //TODO
            //calculate new value
            //return view model
            return View();
        }
    }
}
