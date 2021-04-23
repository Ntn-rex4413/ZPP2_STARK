using Microsoft.AspNetCore.Mvc;
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

    // Dummy class - remove before merge
    public class DummySubscriptionsViewModel
    {
        
    }

    // Dummy class - remove before merge
    public class Cryptocurreny
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        //public ICollection<User> Users { get; set; }
    }
}
