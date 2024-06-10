using Api.Dto;
using Api.Repository;
using Common.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Api.Services.Impl
{
    [Service(Scope = "Transient")]
    public class ParametrosService : IParametrosService
    {
        private readonly IConfiguration config;
        private readonly IParametrosRepository ParametroRepository;
        private readonly ILogger<ParametroCosapiService> logger;

        public ParametrosService(IConfiguration config, IParametrosRepository ParametroRepository, ILogger<ParametroCosapiService> logger)
        {
            this.config = config;
            this.ParametroRepository = ParametroRepository;
            this.logger = logger;
        }
        public List<ParametrosDto> ListarParametrosPorModelo(int idModelo)
        {
            try
            {
                return this.ParametroRepository.ListarParametrosPorModelo(idModelo);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch ListarParametrosPorModelo.", ex);
            }
        }
    }
}
