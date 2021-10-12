using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkPlaceController : ControllerBase
    {
        private readonly ILogger<WorkPlaceController> _logger;
        private readonly IWorkPlaceService _placeService;

        public WorkPlaceController(ILogger<WorkPlaceController> logger)
        {
            _logger = logger;
        }
    }
}
