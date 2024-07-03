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
    public class ParametrosRepository : IParametrosRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<ParametrosRepository> logger;
        private readonly DatabaseManager databaseManager;

        public ParametrosRepository(IConfiguration configuration, ILogger<ParametrosRepository> logger, DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public List<ParametrosDto> ListarParametrosPorModelo(int idModelo)
        {
            List<ParametrosDto> lista = new List<ParametrosDto>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdModelo", idModelo);

                //string query = @"
                //SELECT p.parametro_cosapi as parametro_cosapi, pc.descripcion as parametro_descripcion , grupo , parametro , valor, nroversion
                //FROM Parametros p INNER JOIN ParametroCosapi pc ON p.parametro_cosapi = pc.id_parametro_cosapi
                //INNER JOIN 
                //    (
                //        SELECT  parametro_cosapi,  MAX(nroversion) AS max_version
                //        FROM  Parametros
                //        WHERE idModelo = @IdModelo
                //        GROUP BY parametro_cosapi
                //    ) max_versions
                //ON 
                //    p.parametro_cosapi = max_versions.parametro_cosapi 
                //    AND p.nroversion = max_versions.max_version
                //WHERE pc.estado = 1
                //ORDER BY p.parametro_cosapi , pc.descripcion , grupo , valor
                //";
                string query = @"
                SELECT p.parametro_cosapi as parametro_cosapi, pc.descripcion as parametro_descripcion , grupo , parametro , valor, nroversion
                FROM Parametros p INNER JOIN ParametroCosapi pc ON p.parametro_cosapi = pc.id_parametro_cosapi
                WHERE 
                p.idModelo = @IdModelo
                and pc.estado = 1
                and p.estado=1
                ORDER BY p.parametro_cosapi , pc.descripcion , grupo , valor
                ";

                lista = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                   Query<ParametrosDto>(query, parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to ListarParametrosPorModelo.", e);
            }
            return lista;
        }
    }
}
