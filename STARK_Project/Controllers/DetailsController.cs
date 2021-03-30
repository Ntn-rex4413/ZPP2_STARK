using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.CryptoAPIService;

namespace STARK_Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ICryptoService _service;
        public DetailsController(ICryptoService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
