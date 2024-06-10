
using Api.Entities;
using Common.Database.Conexion;
using Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using Common.Attributes;

namespace Api.Repository.Impl
{
    [Service(Scope="Transient")]
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IConfiguration config;
        private readonly ILogger<EmployeeRepository> logger;
        private readonly DatabaseManager databaseManager;

        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger, DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public List<EvaluacionBE> ListarEvaluaciones(EvaluacionBE evaluacionBE)
        {
            var parameters = new OracleDynamicParameters();
            parameters.Add("pEvaluacion", OracleDbType.Int32, ParameterDirection.Input, evaluacionBE.CEvaluacion);
            parameters.Add("curRef", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {                
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                    Query<EvaluacionBE>("silac.PKG_EXAMENES.sp_listar_evaluacion", parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch evaluaciones.", e);
            }
        }

        public List<Dictionary<string, object>> findAllEmployees()
        {
            var parameters = new OracleDynamicParameters();

            try
            {
                return this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).AdvancedQuery("SELECT" +
                    " dbo.Usuario.idUsuario, dbo.Usuario.idRol," +
                    " dbo.Usuario.nombres, dbo.Usuario.apellidoPaterno, dbo.Usuario.apellidoMaterno, dbo.Usuario.usuario" +
                    " FROM         dbo.Usuario" +
                    " join dbo.Rol on (dbo.Usuario.idRol=dbo.Rol.idRol)"+
                    " WHERE" +
                    " usuario='yorodsan' and clave='123456'"
                    ); //SimpleQuery dbpgib_dev.dbo.EMP
            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch employes.", e);
            }
        }

    }

}
