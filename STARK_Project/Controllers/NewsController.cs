using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            //TODO
            //get data from selected rss channel
            var data = new List<RssItem>();
            return View(data);
        }
    }
}
