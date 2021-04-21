using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using STARK_Project.CryptoAPIService;
using STARK_Project.Data;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using STARK_Project.Models;

namespace STARK_Project.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ILogger<SubscriptionsController> _logger;

        private readonly ICryptoService _service;
        private readonly IDbService _dbService;

        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<User> UserManager { get; set; }

        private User _user;

        public SubscriptionsController(ICryptoService service, IDbService dbService)
        {
            _service = service;
            _dbService = dbService;

            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<User>(new UserStore<User>(this.ApplicationDbContext));

            _user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<UserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
        public IActionResult Index()
        {
            if (_user == null)
            {
                return RedirectToAction("Index", "Summary");
            }
            else
            {
                var data = new SubscriptionsViewModel();
                data.WatchedCryptocurrencies = _dbService.GetWatchlist(_user).Result.ToList();
                data.Cryptocurrencies = _service.GetCryptocurrenciesAsync().Result;
                data.Currencies = _service.GetCurrencies();
                return View(data);
            }
        }
    }
}
