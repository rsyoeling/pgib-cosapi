using Api.Common.Attributes.HttpAuthentication;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration config;
        IEmployeeService pagosServices;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(
            IConfiguration config,
            IEmployeeService pagosServices,
            ILogger<EmployeeController> logger)
        {
            this.config = config;
            this.pagosServices = pagosServices;
            this.logger = logger;
        }

        [HttpPreAuthorize("resource-name:permission")]
        [HttpGet]
        public ActionResult<List<Dictionary<string, object>>> findAllEmployees()
        {
            var list = this.pagosServices.findAllEmployees();
            return Ok(list);

        }

    }
}
