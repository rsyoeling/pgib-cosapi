using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Common.Security.Horus
{
    public class HorusHttpClient
    {

        public ValidateTokenResponse ValidateToken(string baseUrl, ValidateTokenRequest request)
        {

            string httpUrl = baseUrl + "/v1/nonspec/oauth2/introspect/validate";
            try
            {
                string datosJSON = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(datosJSON, System.Text.Encoding.UTF8, "application/json");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(httpUrl);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.PostAsync(httpUrl, contentData).Result;
                    var retorno = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ValidateTokenResponse>(retorno);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed while horus was being consumed", ex);
            }
        }

        public ValidatePermissionResponse ValidatePermission(string baseUrl, ValidatePermissionRequest request, string requestId)
        {

            String httpUrl = baseUrl + "/v2/oauth2/token/validate";
            String response = null;
            try
            {
                String datosJSON = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(datosJSON, System.Text.Encoding.UTF8, "application/json");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(httpUrl);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Add("X-HORUS-REQUEST-ID", requestId);
                    HttpResponseMessage httpResponse = client.PostAsync(httpUrl, contentData).Result;
                    response = httpResponse.Content.ReadAsStringAsync().Result;                    
                }
            }
            catch (AggregateException ex)
            {
                if (ex.GetBaseException() is HttpRequestException)
                {
                    throw new Exception("Failed while horus was being consumed: " + httpUrl + ". Is horus online?", ex);
                }
                else
                {
                    throw new Exception("Failed while horus was being consumed: " + httpUrl, ex);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed while horus was being consumed: " + httpUrl, ex);
            }

            try {
                return JsonConvert.DeserializeObject<ValidatePermissionResponse>(response);
            }
            catch (Exception ex) {
                throw new Exception("Horus " + httpUrl + " returned an unexpected json response body: " + response, ex);
            }
            
        }
    }
}
