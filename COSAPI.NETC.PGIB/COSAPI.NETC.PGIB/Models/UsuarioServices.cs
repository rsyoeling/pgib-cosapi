using COSAPI.NETC.PGIB.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COSAPI.NETC.PGIB.Models
{
    #region << Entidad >>
    public class ResponseUsuario
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Usuario> content { get; set; }
    }
    public class Usuario {
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string nombresCompleto { get; set; }
        public string rolNombre { get; set; }
        public string correoElectronico { get; set; }
        public byte status { get; set; }
    }
    public class RequestUsuario {
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string correoElectronico { get; set; }
    }
    public class ResponseUsuarioId
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<UsuarioId> content { get; set; }
    }
    public class UsuarioId {
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string correoElectronico { get; set; }
    }

    public class RequestActUsu
    {
        public int idUsuario { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int idRol { get; set; }
        public string correoElectronico { get; set; }
    }

    #endregion
    public class UsuarioServices
    {
        public static string ListarUsuario()
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/usuario/listarusuario", Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        }
        public static string ListarUsuarioId(int idUsuario)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/usuario/listarusuarioid?idUsuario="+ idUsuario, Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        }
        public static string Insertar_Usuario(RequestUsuario requestUsuario)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/usuario/insertarusuario", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                string str = "{\"idRol\": " + requestUsuario.idRol +
                    ", \"nombres\": \"" + requestUsuario.nombres
                    + "\", \"apellidoPaterno\": \"" + requestUsuario.apellidoPaterno
                    + "\", \"apellidoMaterno\": \"" + requestUsuario.apellidoMaterno
                    + "\", \"usuario\": \"" + requestUsuario.usuario
                    + "\", \"clave\": \"" + requestUsuario.clave
                    + "\", \"correoElectronico\": \"" + requestUsuario.correoElectronico
                    + "\", \"status\": 1}";
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
        public static string Actualizar_Usuario(RequestActUsu requestActUsu)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/usuario/actualizarusuario", Method.Put);
                request.AddHeader("Content-Type", "application/json");
                string str = "{\"idUsuario\": " + requestActUsu.idUsuario + ", \"nombres\": \"" + requestActUsu.nombres + 
                    "\", \"apellidoPaterno\": \"" + requestActUsu.apellidoPaterno + 
                    "\", \"apellidoMaterno\": \"" + requestActUsu.apellidoMaterno + 
                    "\", \"idRol\": " + requestActUsu.idRol + 
                    ", \"correoElectronico\": \"" + requestActUsu.correoElectronico + "\"}";
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
        public static string Eliminar_Usuario(int idUsuario, byte estatus)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/usuario/eliminarusuario?idUsuario="+ idUsuario+ "&estatus="+ estatus, Method.Put);
                request.AddHeader("Content-Type", "application/json");
                //string str = "{\"idUsuario\": " + idUsuario + ", \"estatus\": " + estatus + "}";
                //var body = str;
                //request.AddStringBody(body, DataFormat.Json);
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
