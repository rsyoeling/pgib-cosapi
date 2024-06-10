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
    public class ParametroCosapiService : IParametroCosapiService
    {
        private readonly IConfiguration config;
        private readonly IParametroCosapiRepository ParametroCosapiRepository;
        private readonly ILogger<ParametroCosapiService> logger;

        public ParametroCosapiService(IConfiguration config, IParametroCosapiRepository ParametroCosapiRepository, ILogger<ParametroCosapiService> logger)
        {
            this.config = config;
            this.ParametroCosapiRepository = ParametroCosapiRepository;
            this.logger = logger;
        }

        public List<ParametroCosapiDto> ListarParametrosCosapi()
        {
            try
            {
                return this.ParametroCosapiRepository.ListarParametrosCosapi();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch ListarParametrosCosapi.", ex);
            }
        }
    }
}
