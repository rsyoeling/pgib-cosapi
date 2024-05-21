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
    public class ModelosService : IModelosService
    {
        private readonly IConfiguration config;
        private readonly IModelosRepository ModelosRepository;
        private readonly ILogger<ModelosService> logger;

        public ModelosService(IConfiguration config, IModelosRepository ModelosRepository, ILogger<ModelosService> logger)
        {
            this.config = config;
            this.ModelosRepository = ModelosRepository;
            this.logger = logger;
        }

        public ObjectResult Insertar_Modelos(ModelosRequest modelosRequest) {
            try
            {
                return this.ModelosRepository.Insertar_Modelos(modelosRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Insertar_Modelos.", ex);
            }
        }

    }
}
