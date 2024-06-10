using COSAPI.NETC.PGIB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COSAPI.NETC.PGIB.Controllers
{
    public class ModelosController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ModelosController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private static int idUsuario = 0;
        private static int idRol = 0;
        [HttpGet]
        [Route("modelos/Index/{idProyecto}")]
        public IActionResult Index(int idProyecto)
        {
            var oUsuario = _httpContextAccessor.HttpContext.Session.GetString("oUsuario");
            var oUsuarioDeserializado = JsonConvert.DeserializeObject<List<accountStarted>>(oUsuario);
            idRol = oUsuarioDeserializado[0].idRol;
            idUsuario = oUsuarioDeserializado[0].idUsuario;
            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            ViewBag.IdProyecto = idProyecto;
            ViewBag.listModelos = ModelosServices.ListarModelosPorProyecto(idProyecto);
            return View(Model);
        }

        public JsonResult GetModelos(int idProyecto)
        {
            var modelos = ModelosServices.ListarModelosPorProyecto(idProyecto);
            return Json(modelos);
        }

        [HttpDelete]
        public IActionResult EliminarModelo(int idModelo)
        {
            var resultado = ModelosServices.Eliminar_Modelo(idModelo);
            return Ok(resultado);
        }

        public JsonResult GetParametrosPorModelo(int idModelo)
        {
            var modelos = ParametrosServices.ListarParametrosPorModelo(idModelo);
            return Json(modelos);
        }

        [HttpGet]
        [Route("modelos/Parametros/{idProyecto}")]
        public IActionResult Parametros(int idProyecto)
        {
            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);
            ViewBag.IdProyecto = idProyecto;
            ViewBag.ListParametrosCosapi = ParametroCosapiServices.ListarParametrosCosapi();

            return View(Model);
        }

        [HttpGet]
        [Route("modelos/Parametros/Edit/{idModelo}")]
        public IActionResult EditarParametros(int idModelo)
        {
            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);
            ModeloResponse objModelo = ModelosServices.Buscar_ModeloPorId(idModelo);

            if (objModelo != null)
            {
                ViewBag.ListParametrosCosapi = ParametroCosapiServices.ListarParametrosCosapi();
                ViewBag.IdModelo = idModelo;
                ViewBag.modelo = objModelo;
                return View(Model);
            }

            return Redirect("/Proyectos/Index");
        }


        [HttpPost]
        public IActionResult InsertarModelo([FromBody] ModelosRequest modelosRequest)
        {
            DateTime fechaActual = DateTime.Now;
            modelosRequest.usuarioCreacion = idUsuario;
            modelosRequest.fechaCreacion = fechaActual.ToString("yyyy-MM-dd HH:mm");
            var resultado = ModelosServices.Insertar_Modelo(modelosRequest);
            return Ok(resultado);
        }
    }
}
