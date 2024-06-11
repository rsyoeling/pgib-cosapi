using Api.Dto;
using Api.Services;
using Microsoft.AspNetCore.Http;
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
    [Route("/v1/modelos")]
    public class ModelosController : ControllerBase
    {
        private readonly IConfiguration config;
        IModelosService ModelosService;
        private readonly ILogger<ModelosController> logger;

        public ModelosController(IConfiguration config,
         IModelosService ModelosService,
         ILogger<ModelosController> logger)
        {
            this.config = config;
            this.ModelosService = ModelosService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("/v1/modelos/insertarmodelos")]
        public ActionResult Insertar_Modelos(ModelosRequest modelosRequest)
        {
            var vins = this.ModelosService.Insertar_Modelos(modelosRequest);
            return Ok(vins);
        }

        [HttpPut]
        [Route("/v1/modelos/actualizarmodelos")]
        public ActionResult Actualizar_Modelos(ModelosRequest modelosRequest)
        {
            var vins = this.ModelosService.Actualizar_Modelos(modelosRequest);
            return Ok(vins);
        }


        [HttpGet]
        [Route("/v1/modelos/listarModeloPorProyecto")]
        public ActionResult Listar_ModeloPorProyecto(int idProyecto)
        {
            var list = this.ModelosService.Listar_ModeloPorProyecto(idProyecto);
            return Ok(list);
        }

        [HttpGet]
        [Route("/v1/modelos/buscarModeloPorId")]
        public ActionResult Buscar_ModeloPorId(int idModelo)
        {
            var list = this.ModelosService.Buscar_ModeloPorId(idModelo);
            return Ok(list);
        }

        [HttpDelete]
        [Route("/v1/modelos/eliminarModelo")]
        public ActionResult Eliminar_Modelos(int idModelo)
        {
            var vins = this.ModelosService.EliminarModelo(idModelo);
            return Ok(vins);
        }

    }
}
