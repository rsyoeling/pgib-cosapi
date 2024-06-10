using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("/v1/parametros")]
    public class ParametrosController : ControllerBase
    {
        private readonly IConfiguration config;
        IParametrosService ParametrosService;
        private readonly ILogger<ParametrosController> logger;

        public ParametrosController(IConfiguration config,
            IParametrosService ParametrosService,
            ILogger<ParametrosController> logger)
        {
            this.config = config;
            this.ParametrosService = ParametrosService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/v1/parametros/listarparametrosPorModelo")]
        public ActionResult listarparametrosPorModelo(int idModelo)
        {
            var list = this.ParametrosService.ListarParametrosPorModelo(idModelo);
            return Ok(list);
        }
    }
}
