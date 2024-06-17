using COSAPI.NETC.PGIB.Entities;
using COSAPI.NETC.PGIB.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace COSAPI.NETC.PGIB.Models
{
    public class AvanceServices
    {
        public static ObjectResultEntity GuardarAvance(Avance obj)
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
                var request = new RestRequest("/v1/avance/insertarAvance", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                string body = JsonConvert.SerializeObject(obj);

                request.AddStringBody(body, DataFormat.Json);

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

        public static List<Avance> ListarAvancesPorModelo(int idModelo)
        {
            List<Avance> lista = new List<Avance>();
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/avance/listarAvancesPorModelo?idModelo=" + idModelo, Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;

                lista = (List<Avance>)JsonConvert.DeserializeObject<List<Avance>>(respuesta);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }
    }
}
