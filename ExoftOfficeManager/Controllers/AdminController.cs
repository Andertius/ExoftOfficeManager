﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
    }
}
