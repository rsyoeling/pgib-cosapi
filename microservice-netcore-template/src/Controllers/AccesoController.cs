using Api.Dto;
using Api.Services;
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
    [Route("/v1/Acceso")]
    public class AccesoController : Controller
    {
        private readonly IConfiguration config;
        IAccesoService accesoService;
        private readonly ILogger<AccesoController> logger;

        public AccesoController(IConfiguration config,
            IAccesoService accesoService,
            ILogger<AccesoController> logger)
        {
            this.config = config;
            this.accesoService = accesoService;
            this.logger = logger;
        }
        [HttpGet]
        [Route("/v1/acceso/accesoMenu")]
        public ActionResult AccesoMenu(int idRol)
        {
            var list = this.accesoService.AccesoMenu(idRol);
            return Ok(list);
        }
        [HttpPut]
        [Route("/v1/acceso/actualizaracceso")]
        public ActionResult Actualizar_AccesoPorRol(AccesoRequest accesoRequest)
        {
            var list = this.accesoService.Actualizar_AccesoPorRol(accesoRequest);
            return Ok(list);
        }

    }
}
