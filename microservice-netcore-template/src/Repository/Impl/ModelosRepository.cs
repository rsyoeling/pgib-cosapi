using Api.Dto;
using Api.Entities;
using Common.Attributes;
using Common.Database.Conexion;
using Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.Impl
{
    [Service(Scope = "Transient")]
    public class ModelosRepository : IModelosRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<ModelosRepository> logger;
        private readonly DatabaseManager databaseManager;

        public ModelosRepository(IConfiguration configuration, ILogger<ModelosRepository> logger, DatabaseManager databaseManager) {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public ObjectResult Insertar_Modelos(ModelosRequest modelosRequest)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                int idParametrizado = 0;
                var tableName = "Modelos";
                var data = new Dictionary<string, object>
                {
                    { "idProyectos", modelosRequest.idProyectos },
                    { "modelo", "'" + modelosRequest.modelo + "'" },
                    { "disciplina", "'" + modelosRequest.disciplina + "'"},
                    { "estatus", "'" + modelosRequest.estatus + "'" },
                    { "urn", "'" + modelosRequest.urn + "'" },
                    { "modeloversion", modelosRequest.modeloversion }, //lógica backend
                    { "estado", "'1'" },
                    { "usuarioCreacion", modelosRequest.usuarioCreacion },
                    { "fechaCreacion", "'" + modelosRequest.fechaCreacion + "'" },
                };

                int IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertDataId(tableName, data);

                if (IdInsertada > 0)
                {
                    foreach (var item in modelosRequest.Parametros)
                    {
                        var tableParam = "Parametros";
                        var dataParam = new Dictionary<string, object>
                        {
                            { "idProyectos", modelosRequest.idProyectos },
                            { "idModelo", IdInsertada },
                            { "parametro_cosapi", "'" + item.parametro_cosapi + "'"},
                            { "grupo", "'" + item.grupo + "'"},
                            { "parametro", "'" + item.parametro + "'"},
                            { "valor", "'" + item.valor + "'"},
                            { "usuarioCreacion", modelosRequest.usuarioCreacion },
                            { "fechaCreacion", "'" + modelosRequest.fechaCreacion + "'" },
                        };

                        idParametrizado = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertData(tableParam, dataParam);
                    }
                }

                result.code = 200000;
                result.message = "success";
                result.content = idParametrizado;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Insertar_Modelos.", e);
            }
            return result;
        }
    }
}
