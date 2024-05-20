using Common.Attributes;
using Common.Database.Conexion;
using Common.Logging;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using Api.Common.Schedulers;

namespace Api.Schedulers
{

    [Scheduler(JobName = "SimpleJob1", TriggerGroup = "SimpleJob1Trigger", CronExpression = "Api.Schedulers.SimpleJob1:CronExpression")]
    public class SimpleJob1 : IJob
    {

        private readonly IServiceProvider provider;
        private DatabaseManager databaseManager;
        private ILogger<SimpleJob1> logger;
        public SimpleJob1(IServiceProvider provider, DatabaseManager databaseManager, ILogger<SimpleJob1> logger)
        {
            this.provider = provider;
            this.databaseManager = databaseManager;
            this.logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var requestId = Guid.NewGuid().ToString();//propage this value to any other services
            LoggerUtil.initializeForNonHttp(requestId);
            //var dbConnector = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId);
            //this.logger.LogInformation("Database connection \n" + string.Join(Environment.NewLine, dbConnector.ConnectionValidationQuery()));
            this.logger.LogInformation("Hello, Im the job execution");
            return Task.CompletedTask;
        }

    }
}
