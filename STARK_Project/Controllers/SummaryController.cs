﻿using Microsoft.AspNetCore.Mvc;
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
            return View();

        }
    }
}
