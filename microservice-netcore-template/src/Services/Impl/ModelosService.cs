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

        public ObjectResult Actualizar_Modelos(ModelosRequest modelosRequest)
        {
            try
            {
                return this.ModelosRepository.Actualizar_Modelos(modelosRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Actualizar_Modelos.", ex);
            }
        }

        public ObjectResult EliminarModelo(int idModelo)
        {
            try
            {
                return this.ModelosRepository.EliminarModelo(idModelo);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Insertar_Modelos.", ex);
            }
        }

        public List<ModeloResponse> Listar_ModeloPorProyecto(int idProyecto)
        {
            try
            {
                return this.ModelosRepository.Listar_ModeloPorProyecto(idProyecto);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Listar_ModeloPorProyecto.", ex);
            }
            
        }

        public ModeloResponse Buscar_ModeloPorId(int idModelo)
        {
            try
            {
                return this.ModelosRepository.Buscar_ModeloPorId(idModelo);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Buscar_ModeloPorId.", ex);
            }
        }
    }
}
