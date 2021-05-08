using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            const string url = "https://minergate.com/blog/feed";
            var data = XElement.Load(url).Descendants("item").Select(i => new RssItem
            {
                Title = i.Element("title").Value,
                Description = i.Element("description").Value,
                PubDate = i.Element("pubDate").Value
            }).ToList();
            return View(data);
        }
    }
}
