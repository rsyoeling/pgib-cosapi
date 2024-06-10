using COSAPI.NETC.PGIB.Utils;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System;

namespace COSAPI.NETC.PGIB.Models
{
    public class ParametroCosapi
    {
        public int idParametroCosapi { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
        public int usuarioCreacion { get; set; }
        public int usuarioModificacion { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaModificacion { get; set; }
    }

    public class ParametroCosapiServices
    {
        public static List<ParametroCosapi> ListarParametrosCosapi()
        {
            List<ParametroCosapi> lista = new List<ParametroCosapi>();
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/parametrocosapi/listarparametros", Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;

                lista = (List<ParametroCosapi>)JsonConvert.DeserializeObject<List<ParametroCosapi>>(respuesta);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

    }
}
