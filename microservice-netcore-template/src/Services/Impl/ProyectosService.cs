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
    public class ProyectosService : IProyectosService
    {
        private readonly IConfiguration config;
        private readonly IProyectosRepository ProyectosRepository;
        private readonly ILogger<ProyectosService> logger;

        public ProyectosService(IConfiguration config, IProyectosRepository ProyectosRepository, ILogger<ProyectosService> logger) {
            this.config = config;
            this.ProyectosRepository = ProyectosRepository;
            this.logger = logger;
        }

        public ObjectResult ListarProyectos(int idUser)
        {
            try
            {
                return this.ProyectosRepository.ListarProyectos(idUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch ListarProyectos.", ex);
            }
        }
        public ObjectResult Insertar_Proyectos(ProyectosRequest proyectosRequest)
        {
            try
            {
                return this.ProyectosRepository.Insertar_Proyectos(proyectosRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Insertar_Proyectos.", ex);
            }
        }
    }
}
