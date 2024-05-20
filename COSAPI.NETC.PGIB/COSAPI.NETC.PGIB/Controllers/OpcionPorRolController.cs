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
    public class OpcionPorRolController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OpcionPorRolController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var oUsuario = _httpContextAccessor.HttpContext.Session.GetString("oUsuario");
            var oUsuarioDeserializado = JsonConvert.DeserializeObject<List<accountStarted>>(oUsuario);
            var idRol = oUsuarioDeserializado[0].idRol;

            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            string listarAccesoMenu = AccountServices.ListarAccesoMenu(idRol);
            Models.ObjectResultAc VBAccesoMenu = JsonConvert.DeserializeObject<Models.ObjectResultAc>(listarAccesoMenu);
            ViewBag.ListarAccesoMenu = VBAccesoMenu;

            return View(Model);
        }

        public IActionResult ActualizarListado(int idRol)
        {
            string listarAccesoMenu = AccountServices.ListarAccesoMenu(idRol);
            Models.ObjectResultAc VBAccesoMenu = JsonConvert.DeserializeObject<Models.ObjectResultAc>(listarAccesoMenu);
            var grupos = VBAccesoMenu.content.GroupBy(item => item.menuNombre);
            var html = "";
            foreach (var grupo in grupos)
            {
                html += "<tr>";
                html += "<td style='display: none;'>" + grupo.First().idMenu + "</td>";
                html += "<td rowspan='" + grupo.Count() + "'>" + grupo.Key + "</td>";
                html += "<td style='display: none;'>" + grupo.First().idSubmenu + "</td>";
                html += "<td>" + grupo.First().submenuNombre + "</td>";
                html += "<td><input type='checkbox' " + (grupo.First().acceso == "1" ? "checked" : "") + "></td>";
                html += "</tr>";

                foreach (var item in grupo.Skip(1))
                {
                    html += "<tr>";
                    html += "<td style='display: none;'>" + item.idMenu + "</td>";
                    html += "<td style='display: none;'>" + item.idSubmenu + "</td>";
                    html += "<td>" + item.submenuNombre + "</td>";
                    html += "<td><input type='checkbox' " + (item.acceso == "1" ? "checked" : "") + "></td>";
                    html += "</tr>";
                }
            }

            return Content(html, "text/html");
        }
        [HttpPut]
        public IActionResult ActualizarAcceso([FromBody] List<contentAcceso> requestAcceso) {
            
            string actualizarAcceso = "";
            foreach (var item in requestAcceso)
            {
                actualizarAcceso = AccountServices.Actualizar_Acceso(item.idRol, item.idMenu, item.idSubmenu, item.acceso);
                //jsonActualizarAcceso = JsonConvert.DeserializeObject<Models.ObjResActualizar>(actualizarAcceso);
            }
            return Ok(actualizarAcceso);
        }

    }
}
