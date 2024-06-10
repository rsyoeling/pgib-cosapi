using Api.Dto;
using Common.Attributes;
using Common.Database.Conexion;
using Constants;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Api.Repository.Impl
{
    [Service(Scope = "Transient")]
    public class ParametroCosapiRepository : IParametroCosapiRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<ParametroCosapiRepository> logger;
        private readonly DatabaseManager databaseManager;

        public ParametroCosapiRepository(IConfiguration configuration, ILogger<ParametroCosapiRepository> logger, DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public List<ParametroCosapiDto> ListarParametrosCosapi()
        {
            List<ParametroCosapiDto> lista = new List<ParametroCosapiDto>();
            try
            {
                string query = @"
                    SELECT id_parametro_cosapi as idParametroCosapi, descripcion
                    FROM ParametroCosapi
                    WHERE estado = 1
                ";

                lista = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                   Query<ParametroCosapiDto>(query, null);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to ListarParametrosCosapi.", e);
            }
            return lista;
        }
    }
}
