using COSAPI.NETC.PGIB.Entities;
using COSAPI.NETC.PGIB.Models;
using COSAPI.NETC.PGIB.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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

            var submenuToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Proyectos/Index");
            var usuarioToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Usuario/Index");
            var permisoToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../OpcionPorRol/Index");
            var perfilToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Perfil/Index");

            if (submenuToUpdate != null)
            {
                submenuToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Proyectos/Index";
            }
            if (usuarioToUpdate != null)
            {
                usuarioToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Usuario/Index";
            }
            if (permisoToUpdate != null)
            {
                permisoToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/OpcionPorRol/Index";
            }
            if (perfilToUpdate != null)
            {
                perfilToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Perfil/Index";
            }

            ViewBag.IdProyecto = idProyecto;
            ViewBag.listModelos = ModelosServices.ListarModelosPorProyecto(idProyecto);
            ViewBag.UrlBaseFrontEnd = ConstantesApp.UrlBaseFrontEnd;
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

            var submenuToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Proyectos/Index");
            var usuarioToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Usuario/Index");
            var permisoToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../OpcionPorRol/Index");
            var perfilToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Perfil/Index");
            if (submenuToUpdate != null)
            {
                submenuToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Proyectos/Index";
            }
            if (usuarioToUpdate != null)
            {
                usuarioToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Usuario/Index";
            }
            if (permisoToUpdate != null)
            {
                permisoToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/OpcionPorRol/Index";
            }
            if (perfilToUpdate != null)
            {
                perfilToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Perfil/Index";
            }

            ViewBag.IdProyecto = idProyecto;
            ViewBag.ListParametrosCosapi = ParametroCosapiServices.ListarParametrosCosapi();
            ViewBag.UrlBaseFrontEnd = ConstantesApp.UrlBaseFrontEnd;
            return View(Model);
        }

        [HttpGet]
        [Route("modelos/Parametros/Edit/{idModelo}")]
        public IActionResult EditarParametros(int idModelo)
        {
            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            var submenuToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Proyectos/Index");
            var usuarioToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Usuario/Index");
            var permisoToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../OpcionPorRol/Index");
            var perfilToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Perfil/Index");
            if (submenuToUpdate != null)
            {
                submenuToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Proyectos/Index";
            }
            if (usuarioToUpdate != null)
            {
                usuarioToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Usuario/Index";
            }
            if (permisoToUpdate != null)
            {
                permisoToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/OpcionPorRol/Index";
            }
            if (perfilToUpdate != null)
            {
                perfilToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Perfil/Index";
            }

            ModeloResponse objModelo = ModelosServices.Buscar_ModeloPorId(idModelo);

            if (objModelo != null)
            {
                ViewBag.ListParametrosCosapi = ParametroCosapiServices.ListarParametrosCosapi();
                ViewBag.IdModelo = idModelo;
                ViewBag.modelo = objModelo;
                ViewBag.UrlBaseFrontEnd = ConstantesApp.UrlBaseFrontEnd;
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

        [HttpPost]
        public IActionResult ActualizarModelo([FromBody] ModelosRequest modelosRequest)
        {
            DateTime fechaActual = DateTime.Now;
            modelosRequest.usuarioModificacion = idUsuario;
            modelosRequest.fechaModificacion = fechaActual.ToString("yyyy-MM-dd HH:mm"); //:ss
            var resultado = ModelosServices.Actualizar_Modelo(modelosRequest);
            return Ok(resultado);
        }


        [HttpGet]
        [Route("modelos/Avance/{idModelo}")]
        public IActionResult Avance(int idModelo)
        {
            string menuSubmenu = AccountServices.Cargar_Menu_SubMenu_Por_Rol(idRol);
            Models.ObjectResultMS Model = JsonConvert.DeserializeObject<Models.ObjectResultMS>(menuSubmenu);

            var submenuToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Proyectos/Index");
            var usuarioToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Usuario/Index");
            var permisoToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../OpcionPorRol/Index");
            var perfilToUpdate = Model.content.subMenu.FirstOrDefault(s => s.urlPagina == "../Perfil/Index");
            if (submenuToUpdate != null)
            {
                submenuToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Proyectos/Index";
            }
            if (usuarioToUpdate != null)
            {
                usuarioToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Usuario/Index";
            }
            if (permisoToUpdate != null)
            {
                permisoToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/OpcionPorRol/Index";
            }
            if (perfilToUpdate != null)
            {
                perfilToUpdate.urlPagina = ConstantesApp.UrlBaseFrontEnd + "/Perfil/Index";
            }

            ModeloResponse objModelo = ModelosServices.Buscar_ModeloPorId(idModelo);

            if (objModelo != null)
            {
                ViewBag.IdModelo = idModelo;
                ViewBag.modelo = objModelo;
                return View(Model);
            }

            return Redirect("/Proyectos/Index");
        }

        public JsonResult DarAvancesModelo(int id_modelo, int avance, string e_avance, int id_elemento, 
            string f_ejecucion, string f_planificada)
        {
            DateTime fechaActual = DateTime.Now;
            Avance obj = new Avance();
            obj.id_modelo = id_modelo;
            obj.avance = avance;
            obj.e_avance = e_avance;
            obj.elemento = id_elemento;
            obj.f_ejecucion = f_ejecucion;
            obj.f_planificada = f_planificada;
            obj.id_usuarioCreacion = idUsuario;

            ObjectResultEntity result = AvanceServices.GuardarAvance(obj);
            return Json(result);
        }

        public JsonResult ListarAvancesPorModelo(int idModelo)
        {
            var result = AvanceServices.ListarAvancesPorModelo(idModelo);
            return Json(result);
        }

        //[HttpGet]
        //[Route("modelos/download")]
        //public IActionResult DownloadExcel()
        //{
        //    //var filePath = "../xlsx/ExcelFile.xlsx";
        //    var fileNamee = Path.GetFileName("ExcelFile.xlsx");
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/xlsx", fileNamee);

        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound("File not found.");
        //    }

        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    var fileName = "DownloadedExcelFile.xlsx";

        //    // Load the Excel file into a memory stream

        //    // Set the EPPlus license context
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    //using (var stream = new MemoryStream(fileBytes))
        //    using (var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath)))
        //    {
        //        using (var package = new ExcelPackage(stream))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0]; // Assuming the first worksheet

        //            if (worksheet != null && worksheet.Dimension != null)
        //            {

        //                // Dynamically paint the columns
        //                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        //                {
        //                    var headerCell = worksheet.Cells[1, col];
        //                    headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    headerCell.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
        //                    headerCell.Style.Font.Color.SetColor(Color.DarkBlue);
        //                }
        //            }
        //            else
        //            {
        //                // Manejar el caso en que worksheet o worksheet.Dimension sea nulo
        //            }

        //            // Save the changes to the memory stream
        //            var memoryStream = new MemoryStream();
        //            package.SaveAs(memoryStream);
        //            memoryStream.Position = 0;

        //            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //        }
        //    }
        //}
        [HttpGet]
        [Route("modelos/download")]
        public IActionResult DownloadExcel()
        {
            // Obtener los datos de ParametroCosapiServices.ListarParametrosCosapi()
            var parametros = ParametroCosapiServices.ListarParametrosCosapi();

            // Set the EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Crear un nuevo archivo Excel en memoria
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ParametrosCosapi");

                // Definir los encabezados de columna
                string[] columnHeaders = { "idParametroCosapi", "descripcion", "estado", "usuarioCreacion", "usuarioModificacion", "fechaCreacion", "fechaModificacion" };

                // Agregar encabezados de columna a la primera fila del worksheet
                for (int col = 0; col < columnHeaders.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = columnHeaders[col];
                    var headerCell = worksheet.Cells[1, col + 1];
                    headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerCell.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    headerCell.Style.Font.Color.SetColor(Color.DarkBlue);
                }
                DateTime fechaActual = DateTime.Now;
                string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");
                // Llenar el resto de filas con los datos obtenidos
                for (int row = 0; row < parametros.Count; row++)
                {
                    var parametro = parametros[row];
                    worksheet.Cells[row + 2, 1].Value = parametro.idParametroCosapi;
                    worksheet.Cells[row + 2, 2].Value = parametro.descripcion;
                    worksheet.Cells[row + 2, 3].Value = "1"; // parametro.estado;
                    worksheet.Cells[row + 2, 4].Value = 0; //parametro.usuarioCreacion;
                    worksheet.Cells[row + 2, 5].Value = ""; //parametro.usuarioModificacion;
                    worksheet.Cells[row + 2, 6].Value = fechaFormateada;// parametro.fechaCreacion != null ? parametro.fechaCreacion.ToString() : string.Empty;
                    worksheet.Cells[row + 2, 7].Value = parametro.fechaModificacion != null ? parametro.fechaModificacion.ToString() : string.Empty;
                }

                // Guardar los cambios en un MemoryStream
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;

                // Descargar el archivo Excel generado
                var fileName = "ParametrosCosapi.xlsx";
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

    }
}
