using Api.Common.Attributes.HttpAuthentication;
using Api.Dto;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/avance")]
    public class AvanceController : ControllerBase
    {
        private readonly IConfiguration config;
        IAvanceService AvanceService;
        private readonly ILogger<AvanceController> logger;

        public AvanceController(IConfiguration config,
            IAvanceService AvanceService,
            ILogger<AvanceController> logger)
        {
            this.config = config;
            this.AvanceService = AvanceService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("/v1/avance/insertarAvance")]
        public ActionResult Insertar_Modelos(AvanceDto obj)
        {
            var vins = this.AvanceService.Insertar_Avance(obj);
            return Ok(vins);
        }

        [HttpGet]
        [Route("/v1/avance/listarAvancesPorModelo")]
        public ActionResult Listar_AvancesPorModelo(int idModelo)
        {
            var list = this.AvanceService.Listar_AvancesPorModelo(idModelo);
            return Ok(list);
        }

    }
}
