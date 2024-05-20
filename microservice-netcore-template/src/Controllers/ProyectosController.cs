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
    [Route("/v1/proyectos")]
    public class ProyectosController : ControllerBase
    {
        private readonly IConfiguration config;
        IProyectosService ProyectosService;
        private readonly ILogger<ProyectosController> logger;

        public ProyectosController(IConfiguration config,
            IProyectosService ProyectosService,
            ILogger<ProyectosController> logger)
        {
            this.config = config;
            this.ProyectosService = ProyectosService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/v1/proyectos/listarproyectos")]
        public ActionResult ListarProyectos(int idUser)
        {
            var list = this.ProyectosService.ListarProyectos(idUser);
            return Ok(list);
        }

        [HttpPost]
        [Route("/v1/proyectos/insertarproyectos")]
        public ActionResult InsertarProyectos(ProyectosRequest proyectosRequest)
        {
            var vins = this.ProyectosService.Insertar_Proyectos(proyectosRequest);
            return Ok(vins);
        }
    }
}
