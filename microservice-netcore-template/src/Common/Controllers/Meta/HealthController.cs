using Common.Configuration;
using Common.Constants;
using Common.Database.Conexion;
using Common.FileDir;
using Common.Http;
using Common.Memory;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Common.Meta.Controllers
{
    [ApiController]
    [Route("v1/meta/health")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> logger;
        private readonly HttpUtil httpUtil = new HttpUtil();
        private readonly FileUtil fileUtil = new FileUtil();
        private readonly MemoryMetricsClient memoryMetricsClient = new MemoryMetricsClient();
        private readonly IConfiguration configuration;
        private readonly ApiGlobalConfiguration apiGlobalConfiguration;
        private DatabaseManager databaseManager;

        public HealthController(IConfiguration configuration, ILogger<HealthController> logger, ApiGlobalConfiguration apiGlobalConfiguration, DatabaseManager databaseManager)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.apiGlobalConfiguration = apiGlobalConfiguration;
            this.databaseManager = databaseManager;
        }

        /*
         * https://docs.spring.io/spring-boot/docs/current/reference/html/actuator.html
         * https://dzone.com/articles/system-memory-health-check-for-aspnet-core
         */
        [HttpGet]
        public IActionResult GetApiHealth(string apiKey)
        {
            bool hasUnrecoverableError = false;
            var expectedMetaApiKeyValue = configuration["META_API_KEY"];

            if (String.IsNullOrEmpty(expectedMetaApiKeyValue) || String.IsNullOrWhiteSpace(expectedMetaApiKeyValue))
            {
                return Ok(httpUtil.createHttpResponse(401010, "Invalid meta api key", null));
            }

            if (!httpUtil.hasRequiredHeader(ApiGlobalConstants.MetaApiKeyHeaderName, Request.Headers, expectedMetaApiKeyValue))
            {
                if (apiKey != expectedMetaApiKeyValue)
                {
                    return Ok(httpUtil.createHttpResponse(401010, "Invalid meta api key", null));
                }
            }

            Dictionary<string, object> health = new Dictionary<string, object>();

            long beforePing = (long)(DateTime.UtcNow).Millisecond;
            health.Add("outcomingNetworkAccess", httpUtil.PingHost("www.google.com"));
            long afterPing = (long)(DateTime.UtcNow).Millisecond;
            health.Add("outcomingNetworkAccessResponseTimeMillis", afterPing - beforePing);

            health.Add("diskAccess", fileUtil.IsDirectoryWritable(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), false));
            health.Add("diskFree", fileUtil.GetAvailableFreeSpaceInBytes(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), false) / (1000000 * 1024));

            var ramMetrics = memoryMetricsClient.GetMetrics();
            health.Add("ramTotal", Math.Round(ramMetrics.Total / 1024, 2));
            health.Add("ramUsed", Math.Round(ramMetrics.Used / 1024, 2));
            health.Add("ramFree", Math.Round(ramMetrics.Free / 1024, 2));

            var databaseMetrics = new Dictionary<string, object>();

            IConfigurationSection databasesRootConfiguration = apiGlobalConfiguration.GetValuesFromRootSection("Database");
            IEnumerable<IConfigurationSection> databasesConfiguration = databasesRootConfiguration.GetChildren();
            var count = 0;
            var defaultDatabaseId = "";
            foreach (IConfigurationSection databaseSectionConfiguration in databasesConfiguration)
            {
                defaultDatabaseId = databaseSectionConfiguration.Key;
                count++;
            }

            if(count == 0 || defaultDatabaseId==""){
                this.logger.LogDebug("There are not any database or database alias is not configured in appsettings.json");
            }else if(count > 2){
                this.logger.LogDebug("Health only support 01 database. Current:" + count);
            }else{
                try
                {
                    databaseMetrics = databaseManager.LookupDatabaseConnectorById(defaultDatabaseId).ConnectionValidationQuery();
                    this.logger.LogDebug("database metrics: " + string.Join(Environment.NewLine, databaseMetrics));
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "Failed to get database conectivity metrics");
                    databaseMetrics.Add("stabilizedConnection", false);
                }

                health.Add("databaseConnection", databaseMetrics.GetValueOrDefault("stabilizedConnection"));
                health.Add("databaseResponseTimeMillis", databaseMetrics.GetValueOrDefault("responseTime"));

                if (databaseMetrics.GetValueOrDefault("stabilizedConnection") != null && databaseMetrics.GetValueOrDefault("stabilizedConnection").ToString().ToUpper() == "FALSE")
                {
                    hasUnrecoverableError = true;
                    health.Add("databaseCodeOracleError", databaseMetrics.GetValueOrDefault("codeOracleError"));
                }
            }

            

            if (!String.IsNullOrWhiteSpace(Request.Query["disableResponseSpec"]) && Request.Query["disableResponseSpec"] == "true")
            {
                if (hasUnrecoverableError)
                {
                    return StatusCode(500, health);
                }
                else
                {
                    return Ok(health);
                }
            }
            else
            {
                if (hasUnrecoverableError)
                {
                    return StatusCode(500, httpUtil.createHttpResponse(200000, "success", health));
                }
                else
                {
                    return Ok(httpUtil.createHttpResponse(200000, "success", health));
                }
            }
        }

    }
}
