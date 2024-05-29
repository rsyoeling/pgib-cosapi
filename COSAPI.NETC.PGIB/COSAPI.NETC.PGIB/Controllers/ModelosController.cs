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
        //private static int idUsuario = 0;
        private static int idRol = 0;
        [HttpGet]
        [Route("modelos/Index/{idProyecto}")]
        public IActionResult Index(int idProyecto)
        {
            var oUsuario = _httpContextAccessor.HttpContext.Session.GetString("oUsuario");
            var oUsuarioDeserializado = JsonConvert.DeserializeObject<List<accountStarted>>(oUsuario);
            idRol = oUsuarioDeserializado[0].idRol;

            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            ViewBag.IdProyecto = idProyecto;

            return View(Model);
        }
        [HttpGet]
        [Route("modelos/Parametros/{idProyecto}")]
        public IActionResult Parametros(int idProyecto)
        {
            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);
            ViewBag.IdProyecto = idProyecto;
            return View(Model);
        }

    }
}
