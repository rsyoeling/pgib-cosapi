using COSAPI.NETC.PGIB.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COSAPI.NETC.PGIB.Models
{
    #region << Entidad >>
    public class ObjectResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<accountStarted> content { get; set; }
    }
    public class accountStarted
    {
        public int idUsuario { get; set; }
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
    }
    public class ObjectResultMS
    {
        public int code { get; set; }
        public string message { get; set; }
        public MenuSubmenuResponse content { get; set; }
    }
    public class MenuSubmenuResponse
    {
        public List<Menu> menu { get; set; }
        public List<Submenu> subMenu { get; set; }
    }
    public class Menu
    {
        public int idMenu { get; set; }
        public string menuNombre { get; set; }
    }
    public class Submenu
    {
        public int idMenu { get; set; }
        public string menuNombre { get; set; }
        public int idSubmenu { get; set; }
        public string submenuNombre { get; set; }
        public string urlPagina { get; set; }
    }
    public class ObjectResultAc
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Acceso> content { get; set; }
    }
    public class Acceso
    {
        public int idMenu { get; set; }
        public string menuNombre { get; set; }
        public int idSubmenu { get; set; }
        public string submenuNombre { get; set; }
        public string acceso { get; set; }
    }
    public class ObjResActualizar {
        public int code { get; set; }
        public string message { get; set; }
        public int content { get; set; }
    }
    public class RequestAcceso {
        public List<contentAcceso> content { get; set; }
    }
    public class contentAcceso
    {
        public int idRol { get; set; }
        public int idMenu { get; set; }
        public int idSubmenu { get; set; }
        public string acceso { get; set; }
    }
    #endregion

    public class AccountServices
    {

        public static string Usuario_Login_Sel(string usuario, string clave)
        {
            string respuesta = "";

            try
            {   
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/account/login", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                string str = "{\"usuario\": \""+ usuario + "\", \"clave\": \""+ clave + "\"}";
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
        public static string Cargar_Menu_SubMenu_Por_Rol(int idRol)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/account/MenuSubmenu?idRol="+ idRol.ToString(), Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        }

        #region << Controller Acceso - Opciones por Rol >>
        public static string ListarAccesoMenu(int idRol)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/acceso/accesoMenu?idRol=" + idRol.ToString(), Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }

            return respuesta;
        }
        public static string Actualizar_Acceso(int idRol, int idMenu, int idSubmenu, string acceso)
        {
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/acceso/actualizaracceso", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                string str = "{\"idRol\": " + idRol + ", \"idMenu\": " + idMenu + ", \"idSubmenu\": " + idSubmenu + ", \"acceso\": \"" + acceso + "\"}";
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
        #endregion

    }
}
