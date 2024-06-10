using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/parametrocosapi")]
    public class ParametroCosapiController : ControllerBase
    {
        private readonly IConfiguration config;
        IParametroCosapiService ParametroCosapiService;
        private readonly ILogger<ParametroCosapiController> logger;

        public ParametroCosapiController(IConfiguration config,
            IParametroCosapiService ParametroCosapiService,
            ILogger<ParametroCosapiController> logger)
        {
            this.config = config;
            this.ParametroCosapiService = ParametroCosapiService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/v1/parametrocosapi/listarparametros")]
        public ActionResult ListarProyectos()
        {
            var list = this.ParametroCosapiService.ListarParametrosCosapi();
            return Ok(list);
        }
    }
}
