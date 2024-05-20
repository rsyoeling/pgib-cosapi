using Api.Dto;
using Api.Entities;
using Api.Repository;
using Common.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Impl
{
    [Service(Scope = "Transient")]
    public class AccesoService: IAccesoService
    {
        private readonly IConfiguration config;
        private readonly IAccesoRepository accesoRepository;
        private readonly ILogger<AccesoService> logger;

        public AccesoService(IConfiguration config, IAccesoRepository accesoRepository, ILogger<AccesoService> logger) {
            this.config = config;
            this.accesoRepository = accesoRepository;
            this.logger = logger;
        }

        public ObjectResult AccesoMenu(int idRol)
        {
            try
            {
                return this.accesoRepository.Listar_AccesoPorRol(idRol);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch AccesoMenu.", ex);
            }
        }
        public ObjectResult Actualizar_AccesoPorRol(AccesoRequest accesoRequest)
        {
            try
            {
                return this.accesoRepository.Actualizar_AccesoPorRol(accesoRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Actualizar_AccesoPorRol.", ex);
            }
        }
    }
}
