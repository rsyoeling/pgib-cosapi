using COSAPI.NETC.PGIB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace COSAPI.NETC.PGIB.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProyectosController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private static int idUsuario = 0;
        public IActionResult Index()
        {
            var oUsuario = _httpContextAccessor.HttpContext.Session.GetString("oUsuario");
            var oUsuarioDeserializado = JsonConvert.DeserializeObject<List<accountStarted>>(oUsuario);
            var idRol = oUsuarioDeserializado[0].idRol;
            idUsuario = oUsuarioDeserializado[0].idUsuario;
            var nombres = oUsuarioDeserializado[0].nombres;

            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            string listarProyectos = ProyectosServices.ListarProyectos(idUsuario);
            Models.ObjResListProy VBListaProyectos = JsonConvert.DeserializeObject<Models.ObjResListProy>(listarProyectos);
            
            ViewBag.UsuarioInicioSesion = nombres;
            ViewBag.ListarProyectos = VBListaProyectos.content;

            return View(Model);
        }

        [HttpPost]
        public IActionResult InsertarProyectos([FromBody] ProyectosRequest proyectosRequest)
        {
            var resultado = ProyectosServices.Insertar_Proyectos(proyectosRequest);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Upload()
        {
            
            var cr = HttpContext.Request.Form["crInput"];
            var nombre = HttpContext.Request.Form["nombreInput"];
            var descripcion = HttpContext.Request.Form["descripcionInput"];         
            var file = HttpContext.Request.Form.Files["imageFile"];
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy HH:mm");

            var resultado="";
            ProyectosRequest proyectosRequest = new ProyectosRequest();
            
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img_proy", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                proyectosRequest.cr = cr;
                proyectosRequest.nombre = nombre;
                proyectosRequest.descripcion = descripcion;
                proyectosRequest.imagen = "../img_proy/" + fileName;
                proyectosRequest.usuarioCreacion = idUsuario;
                proyectosRequest.fechaCreacion = fechaFormateada;
                resultado = ProyectosServices.Insertar_Proyectos(proyectosRequest);

            }
            //return BadRequest("No se ha seleccionado ningún archivo.");
            return Ok(resultado);
        }


    }
}
