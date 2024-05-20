using Api.Dto;
using Api.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration config;
        IAccountService AdministracionService;
        private readonly ILogger<AccountController> logger;

        public AccountController(IConfiguration config, 
            IAccountService AdministracionService,
            ILogger<AccountController> logger) {
            this.config = config;
            this.AdministracionService = AdministracionService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("/v1/account/login")]
        public ActionResult Login(LoginRequest loginRequest)
        {
            var list = this.AdministracionService.Login(loginRequest);
            return Ok(list);
        }
        [HttpGet]
        [Route("/v1/account/MenuSubmenu")]
        public ActionResult MenuSubmenu(int idRol)
        {
            var list = this.AdministracionService.MenuSubmenu(idRol);
            return Ok(list);
        }
    }
}
