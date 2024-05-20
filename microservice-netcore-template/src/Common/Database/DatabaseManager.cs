using Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Common.Database.Conexion
{
    public class DatabaseManager
    {

        private readonly Dictionary<string, IDatabaseConnector> connectors = new Dictionary<string, IDatabaseConnector>();
        private readonly Dictionary<string, string> connectionStrings = new Dictionary<string, string>();
        private readonly ILogger<DatabaseManager> logger;
        private readonly ApiGlobalConfiguration apiGlobalConfiguration;

        public DatabaseManager(ApiGlobalConfiguration apiGlobalConfiguration, ILogger<DatabaseManager> logger)
        {
            this.logger = logger;
            this.apiGlobalConfiguration = apiGlobalConfiguration;
        }

        public IDatabaseConnector LookupDatabaseConnectorById(string databaseId)
        {
            return connectors.GetValueOrDefault(databaseId);
        }

        public void Configure()
        {

            IConfigurationSection databasesRootConfiguration = apiGlobalConfiguration.GetValuesFromRootSection("Database");

            if (databasesRootConfiguration == null && !databasesRootConfiguration.Exists())
            {
                this.logger.LogInformation("This application don't have any database configured");
                return;
            }

            IEnumerable<IConfigurationSection> databasesConfiguration = databasesRootConfiguration.GetChildren();

            foreach (IConfigurationSection databaseSectionConfiguration in databasesConfiguration)
            {
                string engine = databaseSectionConfiguration.GetValue<string>("Engine");
                if (engine == "Oracle")
                {
                    this.logger.LogInformation($"Database {databaseSectionConfiguration.Key}: Starting configurations");
                    string connectionString = ConfigureSqlStringConnection(databaseSectionConfiguration);

                    //descomentar
                    //OracleDatabaseConnector databaseConnector = new OracleDatabaseConnector(this, databaseSectionConfiguration.Key);
                    SqlDatabaseConnector databaseConnector = new SqlDatabaseConnector(this, databaseSectionConfiguration.Key);
                    this.connectors.Add(databaseSectionConfiguration.Key, databaseConnector);
                    this.connectionStrings.Add(databaseSectionConfiguration.Key, connectionString);

                    this.logger.LogInformation($"Database {databaseSectionConfiguration.Key}: Was configured successfully");
                }
                else
                {
                    this.logger.LogInformation($"Unsuported database engine {engine}");
                }
            }
        }

        //private string ConfigureOracleStringConnection(IConfigurationSection genericDatabaseParameters)
        //{

        //    var host = genericDatabaseParameters.GetValue<string>("Host");
        //    var port = genericDatabaseParameters.GetValue<int>("Port");
        //    var password = genericDatabaseParameters.GetValue<string>("Password");
        //    var userid = genericDatabaseParameters.GetValue<string>("User");
        //    var databaseName = genericDatabaseParameters.GetValue<string>("ServiceName");

        //    var poolEnabled = genericDatabaseParameters.GetValue<bool>("enablePool");
        //    var minPoolSize = genericDatabaseParameters.GetValue<int>("minPoolSize");
        //    var maxPoolSize = genericDatabaseParameters.GetValue<int>("maxPoolSize");
        //    var incrPoolSize = genericDatabaseParameters.GetValue<int>("incrPoolSize");
        //    var decrPoolSize = genericDatabaseParameters.GetValue<int>("decrPoolSize");
        //    var poolConnTimeout = genericDatabaseParameters.GetValue<int>("connPoolTimeout");
        //    var poolConnLifetime = genericDatabaseParameters.GetValue<int>("connPoolLifetime");

        //    string connectionString = $"Data Source=(DESCRIPTION=(LOAD_BALANCE=yes)(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port})))(CONNECT_DATA=(FAILOVER_MODE=(TYPE=select)(METHOD=basic))(SERVICE_NAME={databaseName})));User Id={userid};Password={password};Pooling={poolEnabled};Min Pool Size={minPoolSize};Max Pool Size={maxPoolSize};Incr Pool Size={incrPoolSize};Decr Pool Size={decrPoolSize};Connection Timeout={poolConnTimeout};Connection Lifetime={poolConnLifetime}";
        //    return connectionString;
        //}

        private string ConfigureSqlStringConnection(IConfigurationSection genericDatabaseParameters)
        {
            var password = genericDatabaseParameters.GetValue<string>("Password");
            var userid = genericDatabaseParameters.GetValue<string>("User");
            var databaseName = genericDatabaseParameters.GetValue<string>("ServiceName");
            var db_dev = "DBPGIB_DEV";

            var poolConnTimeout = genericDatabaseParameters.GetValue<int>("connPoolTimeout");

            string connectionString = $"Data Source={databaseName};Initial Catalog={db_dev};User Id={userid};Password={password}; timeout={poolConnTimeout};";
            return connectionString;
        }

        /* C# and oracle library needs a new connection on every query :S
         * Maybe the error is due to close()
         * If close() is not called on the connection after the query, conection could be singleton
         */
        public DbConnection GetNewOracleConnection(string databaseId)
        {
            string connectionString = this.connectionStrings.GetValueOrDefault(databaseId);
            return new OracleConnection(connectionString);
        }

        public DbConnection GetNewSqlConnection(string databaseId)
        {
            string connectionString = this.connectionStrings.GetValueOrDefault(databaseId);
            return new SqlConnection(connectionString);
        }
    }
}
