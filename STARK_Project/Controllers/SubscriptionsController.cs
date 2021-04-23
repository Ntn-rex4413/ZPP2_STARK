using Microsoft.AspNetCore.Mvc;
using STARK_Project.CryptoAPIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Controllers
{
    public class SubscriptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
