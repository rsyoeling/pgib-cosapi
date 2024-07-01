using COSAPI.NETC.PGIB.Entities;
using COSAPI.NETC.PGIB.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace COSAPI.NETC.PGIB.Models
{
    #region <<Entidad>>

    public class ModelosRequest
    {
        public int idProyectos { get; set; }
        public int idModelo { get; set; }
        public string modelo { get; set; }
        public string disciplina { get; set; }
        public string estatus { get; set; }
        public string urn { get; set; }
        public int usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
        public int usuarioModificacion { get; set; }
        public string fechaModificacion { get; set; }
        public List<Parametros> Parametros { get; set; }
    }

    public class ObjectResultModelo
    {
        public int code { get; set; }
        public string message { get; set; }
        public object content { get; set; }
    }

    public class ModeloResponse
    {
        public int idModelo { get; set; }
        public int idProyectos { get; set; }
        public string modelo { get; set; }
        public string urn { get; set; }
        public int modeloVersion { get; set; }
        public string disciplina { get; set; }
        public string usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }

        public List<Parametros> Parametros { get; set; }
    }

    public class Parametros
    {
        public string parametro_cosapi { get; set; }
        public string grupo { get; set; }
        public string parametro { get; set; }
        public string valor { get; set; }
    }
    #endregion
    public class ModelosServices
    {
        //public static string Insertar_Modelo(ModelosRequest modelosRequest)
        //{
        //    string respuesta = "";

        //    try
        //    {
        //        //http://10.100.94.14/Rest.Pgib
        //        var options = new RestClientOptions("http://localhost:59400")
        //        {
        //            MaxTimeout = -1,
        //        };
        //        var client = new RestClient(options);
        //        var request = new RestRequest("/v1/modelos/insertarmodelos", Method.Post);
        //        request.AddHeader("Content-Type", "application/json");

        //        string strprm = "";
        //        foreach (var item in modelosRequest.Parametros)
        //        {
        //            strprm = "{\"parametro_cosapi\": \"" + item.parametro_cosapi
        //            + "\", \"grupo\": \"" + item.grupo
        //            + "\", \"parametro\": \"" + item.parametro
        //            + "\", \"valor\": \"" + item.valor
        //            + "\"}";
        //        }

        //        string str = "{\"idProyectos\": " + modelosRequest.idProyectos +
        //            ", \"modelo\": \"" + modelosRequest.modelo
        //            + "\", \"disciplina\": \"" + modelosRequest.disciplina
        //            + "\", \"estatus\": \"" + modelosRequest.estatus
        //            + "\", \"urn\": \"" + modelosRequest.urn
        //            + "\", \"usuarioCreacion\": " + modelosRequest.usuarioCreacion
        //            + ", \"fechaCreacion\": \"" + modelosRequest.fechaCreacion
        //            + "\", \"Parametros\": [" + strprm
        //            + "]}";
        //        //string str = Newtonsoft.Json.JsonConvert.SerializeObject(modelosRequest);

        //        var body = str;

        //        request.AddStringBody(body, DataFormat.Json);
        //        //request.AddStringBody(jsonBody, DataFormat.Json);
        //        //request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
        //        RestResponse response = client.Execute(request);
        //        respuesta = response.Content;
        //    }
        //    catch (Exception ex)
        //    {

        //        respuesta = ex.Message;
        //    }

        //    return respuesta;
        //}

        public static string Insertar_Modelo(ModelosRequest modelosRequest)
        {
            string respuesta = "";
            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/modelos/insertarmodelos", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var parametros = modelosRequest.Parametros.Select(item => new
                {
                    parametro_cosapi = item.parametro_cosapi,
                    grupo = item.grupo,
                    parametro = item.parametro,
                    valor = item.valor
                }).ToList();

                var requestBody = new
                {
                    idProyectos = modelosRequest.idProyectos,
                    modelo = modelosRequest.modelo,
                    disciplina = modelosRequest.disciplina,
                    estatus = modelosRequest.estatus,
                    urn = modelosRequest.urn,
                    usuarioCreacion = modelosRequest.usuarioCreacion,
                    fechaCreacion = modelosRequest.fechaCreacion,
                    Parametros = parametros
                };

                request.AddJsonBody(requestBody);
                var response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;
        }

        public static string Actualizar_Modelo(ModelosRequest modelosRequest)
        {
            string respuesta = "";
            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/modelos/actualizarmodelos", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var parametros = modelosRequest.Parametros.Select(item => new
                {
                    parametro_cosapi = item.parametro_cosapi,
                    grupo = item.grupo,
                    parametro = item.parametro,
                    valor = item.valor
                }).ToList();

                var requestBody = new
                {
                    idModelo = modelosRequest.idModelo,
                    idProyectos = modelosRequest.idProyectos,
                    modelo = modelosRequest.modelo,
                    disciplina = modelosRequest.disciplina,
                    estatus = modelosRequest.estatus,
                    urn = modelosRequest.urn,
                    usuarioModificacion = modelosRequest.usuarioModificacion,
                    fechaModificacion = modelosRequest.fechaModificacion,
                    Parametros = parametros
                };

                request.AddJsonBody(requestBody);
                var response = client.Execute(request);
                respuesta = response.Content;
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;
        }
        
        public static List<ModeloResponse> ListarModelosPorProyecto(int idProyecto)
        {
            List<ModeloResponse> lista = new List<ModeloResponse>();
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/modelos/listarModeloPorProyecto?idProyecto=" + idProyecto, Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;

                lista = (List<ModeloResponse>)JsonConvert.DeserializeObject<List<ModeloResponse>>(respuesta);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public static ModeloResponse Buscar_ModeloPorId(int idModelo)
        {
            ModeloResponse obj = null;
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/modelos/buscarModeloPorId?idModelo=" + idModelo, Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;

                obj = (ModeloResponse)JsonConvert.DeserializeObject<ModeloResponse>(respuesta);
            }
            catch (Exception ex)
            {
            }
            return obj;
        }

        public static ObjectResultEntity Eliminar_Modelo(int idModelo)
        {
            ObjectResultEntity result = new ObjectResultEntity();
            string respuesta = "";
            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest($"/v1/modelos/eliminarModelo?idModelo={idModelo}", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                RestResponse response = client.Execute(request);
                respuesta = response.Content;

                result = JsonConvert.DeserializeObject<ObjectResultEntity>(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
                result.message = respuesta;
            }
            return result;
        }

    }
}
