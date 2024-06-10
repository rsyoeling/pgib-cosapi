using COSAPI.NETC.PGIB.Utils;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System;

namespace COSAPI.NETC.PGIB.Models
{
    public class ParametrosServices
    {
        public class ParametroDto
        {
            public string id_parametro_cosapi { get; set; }
            public string parametro_descripcion { get; set; }
            public string grupo { get; set; }
            public string parametro { get; set; }
            public string valor { get; set; }
        }

        public static List<ParametroDto> ListarParametrosPorModelo(int idModelo)
        {
            List<ParametroDto> lista = new List<ParametroDto>();
            string respuesta = "";

            try
            {
                var options = new RestClientOptions(ConstantesApp.UrlBase)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/parametros/listarparametrosPorModelo?idModelo="+idModelo, Method.Get);
                RestResponse response = client.Execute(request);
                respuesta = response.Content;

                lista = (List<ParametroDto>)JsonConvert.DeserializeObject<List<ParametroDto>>(respuesta);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }
    }
}
