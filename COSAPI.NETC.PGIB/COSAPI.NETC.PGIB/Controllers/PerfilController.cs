using COSAPI.NETC.PGIB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace COSAPI.NETC.PGIB.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PerfilController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var oUsuario = _httpContextAccessor.HttpContext.Session.GetString("oUsuario");
            var oUsuarioDeserializado = JsonConvert.DeserializeObject<List<accountStarted>>(oUsuario);
            var idRol = oUsuarioDeserializado[0].idRol;

            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);//1
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            return View(Model);
        }
    }
}
