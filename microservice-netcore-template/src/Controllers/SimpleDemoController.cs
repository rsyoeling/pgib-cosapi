using Api.Common.Error;
using Api.Constants;
using Api.Entities;
using Api.Services;
using Common.Http;
using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("/public")]
    public class SimpleDemoController : ControllerBase
    {
        private readonly HttpUtil httpUtil = new HttpUtil();
        
        private readonly ILogger<SimpleDemoController> logger;
        private ISimpleService simpleService;

        public SimpleDemoController(ILogger<SimpleDemoController> logger, ISimpleService simpleService)
        {
            this.logger = logger;
            this.simpleService = simpleService;
        }

        [HttpGet]
        [Route("person")]
        public IEnumerable<Person> findAllPersons()
        {   
            return this.simpleService.findAllPersons();
        }

        [HttpPost]
        [Route("person")]
        public IActionResult createPerson(Person person)
        {
            this.simpleService.createPerson(person);
            return Ok(httpUtil.createHttpResponse(200000, "success", null));
        }

        [HttpGet]
        [Route("person/error")]
        public IActionResult simulateError()
        {
            throw new ApiException((ApiResponseCodes.StringEmpyError).AsString(EnumFormat.Description), Enums.ToInt32(ApiResponseCodes.StringEmpyError));
        }

        
    }
}
