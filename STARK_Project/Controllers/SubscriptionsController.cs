using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using STARK_Project.CryptoAPIService;
using STARK_Project.DBServices;

namespace STARK_Project.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ILogger<SubscriptionsController> _logger;

        private readonly ICryptoService _service;
        private readonly IDbService _dbService;
        private string _userEmail;
        public SubscriptionsController(ICryptoService service, IDbService dbService, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _dbService = dbService;

            _userEmail = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
        }
        public IActionResult Index()
        {
            if (_userEmail == null)
            {
                return RedirectToAction("Index", "Summary");
            }
            else
            {
                var watchList = _dbService.GetWatchlist(_userEmail).Result;
                return View(watchList);
            }
        }
    }
}
