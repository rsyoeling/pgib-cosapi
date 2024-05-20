# Microservice Netcore Template 1.0.4

The Docker image based on **mcr.microsoft.com/dotnet/aspnet:3.1**

# Requirements

- Visual Studio
- Oracle for test purposes
- [Markdown editor](https://i.ibb.co/vsnjK00/visualcode-install-markdown.png)
  - By default visual don't show markdown files. Just open it with File > Open > File... or drag and drop it on any part of the ide

# Variables

The following variables are required:

| variable name | Scope| sample value | description |
|--|--|--|--|
|DATABASE_ORACLE_HOST | global | 10.10.10.10 |  oracle database host  |
|DATABASE_ORACLE_PORT | global |  1521 |  oracle database port  |
|DATABASE_ORACLE_SERVICE_NAME | global |  xe |  oracle database service name  |
|DATABASE_ORACLE_USER | local | john  |  oracle database user  |
|DATABASE_ORACLE_PASSWORD | local  | secret  |  oracle database password  |
|DATABASE_POOL_ENABLE | local | true  |  enable oracle pool  |
|DATABASE_POOL_MIN_POOL_SIZE | local | 5 |  oracle pool initial size  |
|DATABASE_POOL_MAX_POOL_SIZE | local |  100 |  oracle pool max size  |
|DATABASE_POOL_INCR_POOL_SIZE | local |   5 | oracle pool incremental size  |
|DATABASE_POOL_DECR_POOL_SIZE | local | 5 |  oracle pool decremental size  |
|DATABASE_POOL_CONNECTION_TIMEOUT | local |  60 |  oracle pool connection timeout  |
|DATABASE_POOL_CONNECTION_LIFETIME | local |  60 |  oracle pool connection lifetime  |
|META_ENV | global | development|  enable specific features just for developer mode  |
|META_APP_NAME | local | hello-world-api |  name of your api  |
|META_API_KEY | global |  123456789 | simple key to protect the access to the meta endpoints  |
|META_LOG_PATH | global |  c://log/api.log | abosoluthe path for file based log. On test & prod is /var/log/api.log  |
|META_LOG_BASE_LEVEL | global |  Information | default log level for new code  |
|META_LOG_MICROSOFT_LEVEL | global |  Warning | default log level for microsoft libraries  |
|META_LOG_AWS_ACCESS_KEY_ID | global |  *** | aws acces key  |
|META_LOG_AWS_SECRET_ACCESS_KEY | global | *** |  aws access secret |
|META_LOG_AWS_GROUP_NAME | global | usil-logger | aws group  |
|META_LOG_AWS_STREAM_NAME | global |  test | aws stream  |
|META_LOG_AWS_DEFAULT_REGION | global |  us-east-1 | aws region  |
|HORUS_API_BASE_URL | global | http://horus-changeme.com | horus api base url  |
|ENABLE_SCHEDULERS | local | true | enable auto schedulers auto configuration  |
|TZ | global | America/Lima | time zone required by oracle  |

Set any value in oracle parameters, just for test memory endpoints in which database is not required

---
# Windows Development

- Just open your visual studio and import this project
- Configure the environment variables in the  visual studio
  - Open api debug properties
  - ![debug properties](https://i.ibb.co/MnLsDsr/vs-studio-step1.png)
  - add variables
  - ![add variables](https://i.ibb.co/M9c0shs/vs-studio-step2.png)
- Click on play button "IIS Express"
- Open this url in your browser: http://localhost:63117/swagger

## Linux Development


```
export DATABASE_ORACLE_HOST=192.168.1.60
export DATABASE_ORACLE_PORT=1521
export DATABASE_ORACLE_SERVICE_NAME=xe
export DATABASE_ORACLE_USER=STARK_INDUSTRIES
export DATABASE_ORACLE_PASSWORD=changeme
export DATABASE_POOL_ENABLE=true
export DATABASE_POOL_MIN_POOL_SIZE=5
export DATABASE_POOL_MAX_POOL_SIZE=100
export DATABASE_POOL_INCR_POOL_SIZE=5
export DATABASE_POOL_DECR_POOL_SIZE=5
export DATABASE_POOL_CONNECTION_TIMEOUT=60
export DATABASE_POOL_CONNECTION_LIFETIME=60
export META_ENV=development
export META_APP_NAME=hello-api
export META_API_KEY=123456
export META_LOG_BASE_LEVEL=Information
export META_LOG_MICROSOFT_LEVEL=Warning
export META_LOG_PATH=log/api.log
export TZ=America/Lima 
export ENABLE_SCHEDULERS=false
```

```
tput reset && dotnet restore src/Api.csproj && dotnet build src/Api.csproj -c Release -o build && dotnet publish src/Api.csproj -c Release -o publish && dotnet publish/Api.dll 
```

---
## Linux Development with docker

```
docker run -it -p 8080:80 -v $(pwd):/app --user nobody mcr.microsoft.com/dotnet/sdk:3.1

export DATABASE_ORACLE_HOST=192.168.1.60
export DATABASE_ORACLE_PORT=1521
export DATABASE_ORACLE_SERVICE_NAME=xe
export DATABASE_ORACLE_USER=STARK_INDUSTRIES
export DATABASE_ORACLE_PASSWORD=changeme
export DATABASE_POOL_ENABLE=true
export DATABASE_POOL_MIN_POOL_SIZE=5
export DATABASE_POOL_MAX_POOL_SIZE=100
export DATABASE_POOL_INCR_POOL_SIZE=5
export DATABASE_POOL_DECR_POOL_SIZE=5
export DATABASE_POOL_CONNECTION_TIMEOUT=60
export DATABASE_POOL_CONNECTION_LIFETIME=60
export META_ENV=development
export META_APP_NAME=hello-api
export META_API_KEY=123456
export META_LOG_BASE_LEVEL=Information
export META_LOG_MICROSOFT_LEVEL=Warning
export META_LOG_PATH=log/api.log
export TZ=America/Lima 
export ENABLE_SCHEDULERS=false

cd /app/src && \
dotnet restore "Api.csproj" && dotnet build "Api.csproj" -c Release -o /app/build && \
dotnet publish "Api.csproj" -c Release -o /app/publish && \
dotnet /app/publish/Api.dll

```

---
## Database

If the memory endpoint worked, your next step is use oracle.

For this test, follow these steps:

- Create an Oracle database instance using docker or using any instance
- Create the user: **sql-scripts/security.sql** or use an existing one
- Create the required table: **sql-scripts/ddl.sql**
- Create the required data: **sql-scripts/data.sql**

Update the oracle variables and run. Try the **/employee** endpoint.

**NOTE:** To add another database, 

- check **src/Common/Database/Oracle**
- replicate them
- add the new engine in **src/Common/Database/DatabaseManager.cs Configure()** 
- use that **Engine** in  **src/appsettings.json**

**NOTE:** If your project will not use any database:

- Delete the **Database** section in **src/appsettings.json**

---
# Schedulers

Just add a class inside the **src/Schedulers** folder and add it the **[Scheduler]** annotation with these attributes:

|attribute name | sample value | description
|--|--|--
|JobName |HelloJob| Simple string to identify the job |
|TriggerGroup |HelloTrigger|Simple string to identify the trigger used to schedule the job |
|CronExpresion |0/5 * * ? * * *| String with **7** special positions as maximum. Check the docs https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html |

```
[Scheduler(JobName = "HelloJob", TriggerGroup = "HelloTrigger", CronExpression = "Api.Schedulers.SimpleJob1:CronExpression")]
public class SimpleJob : IJob
{
      public Task Execute(IJobExecutionContext context)
      {
          Console.WriteLine(DateTime.Now.ToString("dd MMM yyyy HH:mm:ss") + " Hello, Job executed");
          return Task.CompletedTask;
      }
}
```

And add these to **src/appsettings.json**

```
"Api.Schedulers.SimpleJob1": {
  "CronExpression": "0/15 * * ? * * *",
  "Enabled": "true"
}
```

Since we are using  **src/appsettings.json**, we could use environment variables to handle the cron expression and to enalbel/disable this scheduler

```
"Api.Schedulers.SimpleJob1": {
  "CronExpression": "$(SIMPLE_JOB_1_CRON_EXPRESSION)",
  "Enabled":  "$(SIMPLE_JOB_1_CRON_ENABLED)",
}
```

**NOTE:** If your project will not use schedulers:

- .\src\Api.csproj
  - `<PackageReference Include="Quartz" Version="3.3.3" />`
- set **ENABLE_SCHEDULERS** to **false**
- Delete the content of **/src/Schedulers**
- Delete all reference to cron expressions in **src/appsettings.json**


Cron generators

- https://www.programmertools.online/generator/cron_expression.html
- http://www.cronmaker.com
- https://en.rakko.tools/tools/88/


Here some cron samples:

|Expression |	Meaning|
|--|--|
|0/5 * * ? * * *|every 5 seconds |
|0 0/5 * * * ? |	every 5 minutes|
|10 0/5 * * * ? |	every 5 minutes, at 10 seconds after the minute (i.e. 10:00:10 am, 10:05:10 am, etc.).|
|0 0 0/1 1/1 * ? * |every 1 hour|
|0 30 10-13 ? * WED,FRI |	fires at 10:30, 11:30, 12:30, and 13:30, on every Wednesday and Friday.|
|0 0/30 8-9 5,20 * ? |	fires every half hour between the hours of 8 am and 10 am on the 5th and 20th of every month|
|0 0 12 * * ? |	Fire at 12pm (noon) every day|
|0 15 10 ? * * |	Fire at 10:15am every day|
|0 15 10 * * ? |	Fire at 10:15am every day|
|0 15 10 * * ? * |	Fire at 10:15am every day|
|0 15 10 * * ? 2005 |	Fire at 10:15am every day during the year 2005|
|0 * 14 * * ? |	Fire every minute starting at 2pm and ending at 2:59pm, every day|
|0 0/5 14 * * ? |	Fire every 5 minutes starting at 2pm and ending at 2:55pm, every day|
|0 0/5 14,18 * * ? |	Fire every 5 minutes starting at 2pm and ending at 2:55pm, AND fire every 5 minutes starting at 6pm and ending at 6:55pm, every day|
|0 0-5 14 * * ? |	Fire every minute starting at 2pm and ending at 2:05pm, every day|
|0 10,44 14 ? 3 WED |	Fire at 2:10pm and at 2:44pm every Wednesday in the month of March.|
|0 15 10 ? * MON-FRI |	Fire at 10:15am every Monday, Tuesday, Wednesday, Thursday and Friday|
|0 15 10 15 * ? |	Fire at 10:15am on the 15th day of every month|
|0 15 10 L * ? |	Fire at 10:15am on the last day of every month|
|0 15 10 ? * 6L |	Fire at 10:15am on the last Friday of every month|
|0 15 10 ? * 6L |	Fire at 10:15am on the last Friday of every month|
|0 15 10 ? * 6L 2002-2005 |	Fire at 10:15am on every last Friday of every month during the years 2002, 2003, 2004 and 2005|
|0 15 10 ? * 6#3 |	Fire at 10:15am on the third Friday of every month|

Pay attention to the effects of '?' and '*' in the day-of-week and day-of-month fields!

---
# Controllers

To add more rest controllers, just add more c# class like **/src/Controllers/EmployeeController.cs**

---
# Services & Repositories

To add more rest services and respositories to be used in controllers, just add more c# class like **/src/Controllers/EmployeeController.cs**

- src/Repository/Impl/EmployeeRepository.cs
- src/Services/Impl/EmployeeService.cs

Don't forget to use **[Service(Scope="Transient")]** or **[Service(Scope="Singleton")]**

---
# Testing & Production

To create the image  execute the following command on the src folder:

```bash
docker build -t name-of-your-api:1.0.0 .
```

### Run image (Linux)

Start your image sending the environment variables:

```bash
docker run --name name-of-your-api -d --rm -p 8080:80 \
-e DATABASE_ORACLE_HOST=192.168.1.60  \
-e DATABASE_ORACLE_PORT=1521  \
-e DATABASE_ORACLE_SERVICE_NAME=xe  \
-e DATABASE_ORACLE_USER=STARK_INDUSTRIES  \
-e DATABASE_ORACLE_PASSWORD=changeme  \
-e DATABASE_POOL_ENABLE=true  \
-e DATABASE_POOL_MIN_POOL_SIZE=5  \
-e DATABASE_POOL_MAX_POOL_SIZE=100  \
-e DATABASE_POOL_INCR_POOL_SIZE=5  \
-e DATABASE_POOL_DECR_POOL_SIZE=5  \
-e DATABASE_POOL_CONNECTION_TIMEOUT=60  \
-e DATABASE_POOL_CONNECTION_LIFETIME=60  \
-e META_ENV=development  \
-e META_APP_NAME=hello-api  \
-e META_API_KEY=123456  \
-e META_LOG_BASE_LEVEL=Information  \
-e META_LOG_MICROSOFT_LEVEL=Warning  \
-e META_LOG_PATH=/var/log/api.log  \
-e ENABLE_SCHEDULERS=false  \
-e TZ=America/Lima   \
name-of-your-api:1.0.0
```

### Run with remote variables (Linux)

Required variables could be configured remotely to avoid shell access.

```
docker run -d --name name-of-your-api -it --rm -p 8080:80 \
-e CONFIGURATOR_GET_VARIABLES_FULL_URL=http://10.10.10.10:2708/api/v1/variables?application=name-of-your-api \
-e CONFIGURATOR_AUTH_HEADER=apiKey:changeme \
-e TZ=America/Lima name-of-your-api:1.0.0
```
---
# Endpoints

Browse to [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html) if your are running with visual studio to view swagger the web page with the endpoints

---
# Features

- **Docker**
- Ready to use netcore template
- Log to file and stdout
- Secure endpoints with horus (oauth2)
- Swagger for development
- /health endpoint

---
# Unit Test

If your code is ready, execute this
```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude="[*]*Startup%2c[*]*Program%2c[*]Common*%2c[*]Api.UnitOfwork*" /p:Threshold=90
```

Result **must be** something like this:

![img](https://i.ibb.co/34ZGTTJ/netcore-unit-test-result.png)

More samples:

- https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1

Class should not be skipped and no one unit test should have error

### Code Coverage

If the unit tests are succeed, you can generate the coverage report:

Install the report generator
```
dotnet tool install --global dotnet-reportgenerator-globaltool
```

And run:
```
reportgenerator "-reports:test\coverage.opencover.xml" "-targetdir:test\coveragereport" -reporttypes:Html
```

If there is no error, you will have several files in **test\coveragereport** folder. Open the **index.html**:

![img](https://i.ibb.co/1rP9ZzM/netcore-coverlet-coverage-report-html.png)

The main idea is to have percentages **greater than 90%**

---
# Log Management

- Always are sent to console, file and aws: Warn & Information
- To enable debug use log management: `v1/meta/log/management?apiKey=123456&baseLogLevel=Debug`
- To enable internal microsoft debug, use log management: `v1/meta/log/management?apiKey=123456&microsoftLogLevel=Debug`

---
# Roadmap

- Add unit test & coverage
- Dependecy injection
- Add NHibernate
- Add remote log
- Add circuit breaker strategy
- Publish to https://github.com/usil

# Contributor

- [JRichardsz](https://github.com/jrichardsz)
