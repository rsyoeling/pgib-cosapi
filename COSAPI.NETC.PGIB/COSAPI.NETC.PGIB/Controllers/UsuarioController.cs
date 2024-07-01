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
    public class UsuarioController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioController(IHttpContextAccessor httpContextAccessor)
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

            string listarUsuario = UsuarioServices.ListarUsuario();
            Models.ResponseUsuario VBUsuario = JsonConvert.DeserializeObject<Models.ResponseUsuario>(listarUsuario);
            ViewBag.ListarUsuario = VBUsuario;

            return View(Model);
        }

        [HttpPost]
        public IActionResult InsertarUsuario([FromBody] RequestUsuario requestUsuario)
        {
            var resultado = UsuarioServices.Insertar_Usuario(requestUsuario);
            return Ok(resultado);
        }

        public IActionResult ActualizarListado()
        {
            string listarUsuario = UsuarioServices.ListarUsuario();
            Models.ResponseUsuario VBUsuario = JsonConvert.DeserializeObject<Models.ResponseUsuario>(listarUsuario);
            var html = "";
            foreach (var fila in VBUsuario.content)
            {
                html += "<tr>";
                html += "<td style='display: none;'>" + fila.idUsuario + "</td>";
                html += "<td>"+ fila.usuario + "</td>";
                html += "<td>" + fila.nombresCompleto + "</td>";
                html += "<td>" + fila.rolNombre + "</td>";
                html += "<td>" + fila.correoElectronico + "</td>";
                html += "<td>" + (fila.status==1?"A":"I") + "</td>";
                html += "<td><button class='btn btn-primary edit-btn' onclick='editbtn(" + fila.idUsuario + ")'>Editar</button>&nbsp";
                html += "<button class='btn btn-danger delete-btn' onclick=\"deletebtn(" + fila.idUsuario + ", '" + (fila.status == 1 ? 'A' : 'I') + "')\">"+ (fila.status == 1 ? "Inactivar" : "Activar") + "</button></td>";
                html += "</tr>";
            }
            return Content(html, "text/html");
        }

        public IActionResult ListarUsuarioId(int idUsuario)
        {
            string listarUsuarioId = UsuarioServices.ListarUsuarioId(idUsuario);
            Models.ResponseUsuarioId VBUsuarioId = JsonConvert.DeserializeObject<Models.ResponseUsuarioId>(listarUsuarioId);
            
            return Ok(VBUsuarioId);
        }

        [HttpPost]
        public IActionResult ActualizarUsuario([FromBody] RequestActUsu requestActUsu)
        {
            var resultado = UsuarioServices.Actualizar_Usuario(requestActUsu);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult EliminarUsuario(int idUsuario, byte estatus)
        {
            var resultado = UsuarioServices.Eliminar_Usuario(idUsuario, estatus);
            return Ok(resultado);
        }
    }
}
