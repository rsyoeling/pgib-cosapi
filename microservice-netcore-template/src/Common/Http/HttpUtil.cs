using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Common.Http
{
    public class HttpUtil
    {
        public String GetHeader(HttpContext context, String key, String defaultValue)
        {
            if (context == null || context.Request == null || context.Request.Headers == null ) {
                return defaultValue;
            }
            String headerValue = context.Request.Headers[key];
            if (String.IsNullOrEmpty(headerValue))
            {
                return defaultValue;
            }
            else {
                return headerValue;
            }
        }

        public string getIp(HttpContext context, String defaultValue)
        {

            string ip = context.Connection.RemoteIpAddress.ToString();            
            try {
                if (ip == "::1")
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
                }
                //TODO: try to get the real ip here!!
                //if request is performed from curl, 
                //the returned value is Wireless LAN adapter Wi-Fi: Link-local IPv6 Address
                //sample aabb::ccdd::Ffgg                
                if (!string.IsNullOrEmpty(ip))
                {
                    return ip;
                }

                ip = GetHeader(context, "HTTP_X_FORWARDED_FOR", null);
                if (!string.IsNullOrEmpty(ip))
                {
                    return ip;
                }

                ip = GetHeader(context, "REMOTE_ADDR", null);
                if (!string.IsNullOrEmpty(ip))
                {
                    return ip;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception ex) {
                Log.Warning(ex, "ip of consumer cannot be determined");
                return defaultValue;
            }
        }

        public bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException ex)
            {
                Log.Error(ex, "there is no internet access");
                pingable = false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public bool hasRequiredHeader(string headerKey, IHeaderDictionary headers, string expectedValue)
        {
            if (String.IsNullOrEmpty(headers[headerKey]) || String.IsNullOrWhiteSpace(headers[headerKey]))
            {
                Log.Debug(headerKey+" header is missing");
                return false;
            }

            if (String.IsNullOrEmpty(expectedValue) || String.IsNullOrWhiteSpace(expectedValue))
            {
                Log.Error("expected value is null or empty");
                return false;
            }

            return headers[headerKey] == expectedValue;
        }

        public Dictionary<string, object> createHttpResponse(int code, string message, Dictionary<string, object> content)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            response.Add("code", code);
            response.Add("message", message);

            if (content != null)
            {
                response.Add("content", content);
            }

            return response;
        }

        public Dictionary<string, object> CreateHttpResponse(int code, string message, object content)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            response.Add("code", code);
            response.Add("message", message);

            if (content != null)
            {
                response.Add("content", content);
            }

            return response;
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var localIp = "";
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                    break;
                }
            }
            if (!String.IsNullOrEmpty(localIp))
            {
                return localIp;
            }
            else {
                Log.Error("No network adapters with an IPv4 address in the system!");
                return "localhost";
            }
            
        }
    }
}
