using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COSAPI.NETC.PGIB.Models
{
    #region <<Entidad>>
    public class ObjResListProy
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Proyectos> content { get; set; }
    }
    public class Proyectos
    {
        public int idProyectos { get; set; }
        public string cr { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public string usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaModificacion { get; set; }
    }
    public class ProyectosRequest
    {
        public string cr { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public int usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
    }
    public class UploadImageModel
    {
        [Required(ErrorMessage = "Por favor selecciona una imagen.")]
        [Display(Name = "Imagen")]
        public IFormFile ImageFile { get; set; }
    }
    #endregion
    public class ProyectosServices
    {
        public static string ListarProyectos(int idUser)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions("http://10.100.94.14/Rest.Pgib")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/proyectos/listarproyectos?idUser=" + idUser, Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        }
        public static string Insertar_Proyectos(ProyectosRequest proyectosRequest)
        {
            string respuesta = "";

            try
            {
                //https://localhost:44388/
                var options = new RestClientOptions("http://10.100.94.14/Rest.Pgib")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/proyectos/insertarproyectos", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                string str = "{\"cr\": \"" + proyectosRequest.cr +
                    "\", \"nombre\": \"" + proyectosRequest.nombre
                    + "\", \"descripcion\": \"" + proyectosRequest.descripcion
                    + "\", \"imagen\": \"" + proyectosRequest.imagen
                    + "\", \"usuarioCreacion\": " + proyectosRequest.usuarioCreacion
                    + ", \"fechaCreacion\": \"" + proyectosRequest.fechaCreacion + "\"}";
                var body = str;
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        }
    }
}
