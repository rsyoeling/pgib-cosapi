using Api.Dto;
using Api.Entities;
using Api.Repository;
using Common.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Api.Services.Impl
{
    [Service(Scope = "Transient")]
    public class AvanceService : IAvanceService
    {
        private readonly IConfiguration config;
        private readonly IAvanceRepository avanceRepository;
        private readonly ILogger<AvanceService> logger;

        public AvanceService(IConfiguration config, IAvanceRepository avanceRepository,
            ILogger<AvanceService> logger)
        {
            this.config = config;
            this.avanceRepository = avanceRepository;
            this.logger = logger;
        }

        public ObjectResult Insertar_Avance(AvanceDto obj)
        {
            try
            {
                return this.avanceRepository.Insertar_Avance(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Insertar_Avance.", ex);
            }
        }

        public List<AvanceDto> Listar_AvancesPorModelo(int idModelo)
        {
            try
            {
                return this.avanceRepository.Listar_AvancesPorModelo(idModelo);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Listar_AvancesPorModelo.", ex);
            }
        }
    }
}
