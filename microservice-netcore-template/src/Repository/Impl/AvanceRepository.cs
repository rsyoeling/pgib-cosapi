using Amazon.Runtime.Internal.Transform;
using Api.Dto;
using Api.Entities;
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
    public class AvanceRepository : IAvanceRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<AvanceRepository> logger;
        private readonly DatabaseManager databaseManager;

        public AvanceRepository(IConfiguration configuration, ILogger<AvanceRepository> logger, DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }
        public ObjectResult Insertar_Avance(AvanceDto obj)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var tableName = "Avance";
                var data = new Dictionary<string, object>
                {
                     { "idModelo", obj.id_modelo },
                     { "idElemento", obj.elemento },
                     { "avance", obj.avance },
                     { "estadoAvance",  "'"  + obj.e_avance +  "'" },
                     { "fechaEjecutada", "'" + obj.f_ejecucion +  "'" },
                     { "fechaPlanificada", "'" + obj.f_planificada +  "'"  },
                     { "fechaCreacion", "GETDATE()" },
                     { "usuarioCreacion", obj.id_usuarioCreacion },
                };

                int IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.
                    osilDatabaseId).InsertDataId(tableName, data);

                result.code = 200000;
                result.message = "success";
                result.content = IdInsertada;

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Insertar_Avance.", e);
            }
            return result;
        }

        public List<AvanceDto> Listar_AvancesPorModelo(int idModelo)
        {
            List<AvanceDto> lista = new List<AvanceDto>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdModelo", idModelo);

                string query = @"
                    select idAvance, idElemento as elemento, avance, estadoAvance as e_avance, 
                    FORMAT(fechaEjecutada, 'dd-MM-yyyy') AS f_ejecucion,
                    FORMAT(fechaPlanificada, 'dd-MM-yyyy') AS f_planificada
                    from Avance
                    where idModelo = @IdModelo";

                lista = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                   Query<AvanceDto>(query, parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to Listar_AvancesPorModelo.", e);
            }
            return lista;
        }
    }
}
