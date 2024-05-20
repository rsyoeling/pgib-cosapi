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
    [Route("/v1/Usuario")]
    public class UsuarioController : Controller
    {
        private readonly IConfiguration config;
        IUsuarioService usuarioService;
        private readonly ILogger<UsuarioController> logger;

        public UsuarioController(IConfiguration config,
            IUsuarioService usuarioService,
            ILogger<UsuarioController> logger) {
            this.config = config;
            this.usuarioService = usuarioService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/v1/usuario/listarusuario")]
        public ActionResult AccesoMenu()
        {
            var list = this.usuarioService.Listar_Usuario();
            return Ok(list);
        }
        [HttpGet]
        [Route("/v1/usuario/listarusuarioid")]
        public ActionResult ListarUsuarioId(int idUsuario)
        {
            var list = this.usuarioService.Listar_Usuario_Por_Id(idUsuario);
            return Ok(list);
        }

        [HttpPost]
        [Route("/v1/usuario/insertarusuario")]
        public ActionResult InsertarUsuario(UsuarioRequest usuarioRequest)
        {
            var vins = this.usuarioService.Insertar_Usuario(usuarioRequest);
            return Ok(vins);
        }

        [HttpPut]
        [Route("/v1/usuario/actualizarusuario")]
        public ActionResult ActualizarUsuario(ActUsuRequest usuarioRequest)
        {
            var vact = this.usuarioService.Actualizar_Usuario(usuarioRequest);
            return Ok(vact);
        }

        [HttpPut]
        [Route("/v1/usuario/eliminarusuario")]
        public ActionResult EliminarUsuario(int idUsuario, byte estatus)
        {
            var veli = this.usuarioService.Eliminar_Usuario(idUsuario, estatus);
            return Ok(veli);
        }
    }
}
