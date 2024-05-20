using Api.Entities;
using Api.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Common.Attributes;

namespace Api.Services.Impl
{
    [Service(Scope="Transient")]
    public class EmployeeService : IEmployeeService
    {

        private readonly IConfiguration config;
        private readonly IEmployeeRepository pagosRepository;
        private readonly ILogger<EmployeeService> logger;

        public EmployeeService(IConfiguration config, IEmployeeRepository pagosRepository,
            ILogger<EmployeeService> logger)
        {
            this.config = config;
            this.pagosRepository = pagosRepository;
            this.logger = logger;
        }

        public List<EvaluacionBE> ListarEvaluaciones(EvaluacionBE evaluacionBE)
        {

            List<EvaluacionBE> lstEvaluaciones;

            try
            {
                lstEvaluaciones = this.pagosRepository.ListarEvaluaciones(evaluacionBE);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error al ListarEvaluaciones: ");
                lstEvaluaciones = new List<EvaluacionBE>();
            }

            return lstEvaluaciones;
        }

        public List<Dictionary<string, object>> findAllEmployees(){
        {
            try
            {
              return this.pagosRepository.findAllEmployees();
            }
            catch (Exception ex)
            {
              throw new Exception("Failed to fetch employes.", ex);
            }
        }

    }}
}
