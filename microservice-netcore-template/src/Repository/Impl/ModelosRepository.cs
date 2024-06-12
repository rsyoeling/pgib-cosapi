using Api.Dto;
using Api.Entities;
using Common.Attributes;
using Common.Database.Conexion;
using Constants;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
                    { "modeloversion", 1 }, //lógica backend modelosRequest.modeloversion
                    { "estado", "'1'" },
                    { "usuarioCreacion", modelosRequest.usuarioCreacion },
                    { "fechaCreacion", "'" + modelosRequest.fechaCreacion + "'" },
                };

                string query = @"
                    SELECT idModelo FROM Modelos
                    where idProyectos = " + modelosRequest.idProyectos + " ORDER BY idModelo DESC ";

                List<ModeloResponse> lista = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                   Query<ModeloResponse>(query, null);
                int IdInsertada = 0;
                if (lista.Count () > 0 ) // Si el proyecto ya cuenta con modelo, solos e actualiza
                {
                    data = new Dictionary<string, object>
                    {
                        { "modelo", modelosRequest.modelo  },
                        { "disciplina",  modelosRequest.disciplina },
                        { "estatus",  modelosRequest.estatus  },
                        { "urn", modelosRequest.urn},
                        { "usuarioModificacion", modelosRequest.usuarioCreacion },
                        { "fechaModificacion", modelosRequest.fechaCreacion  }
                    };
                    var condition = " idProyectos=" + modelosRequest.idProyectos;
                    IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, data , condition);

                    if (IdInsertada > 0)
                    {
                        IdInsertada = lista[0].idModelo;
                    }
                }
                else
                {
                   IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertDataId(tableName, data);
                }

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
                            { "estado", "'1'" },
                            { "nroVersion", "'1'" },
                            { "usuarioCreacion", modelosRequest.usuarioCreacion },
                            { "fechaCreacion",  "'" + modelosRequest.fechaCreacion + "'"  }
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

        public ObjectResult Actualizar_Modelos(ModelosRequest modelosRequest)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                int IdInsertada = 0;
                int idParametrizado = 0;
                var tableName = "Modelos";
                string query = "";

                var dataParam = new Dictionary<string, object>
                    {
                        { "disciplina",  modelosRequest.disciplina },
                        { "usuarioModificacion", modelosRequest.usuarioModificacion },
                        { "fechaModificacion", modelosRequest.fechaModificacion  }
                    };

                var condition = " idModelo=" + modelosRequest.idModelo;
                IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, dataParam, condition);

                if (IdInsertada > 0)
                {
                    // Actualizar Lista anterior estado 0 como version anterior

                     query = "SELECT MAX(nroversion) AS max_version FROM Parametros WHERE idModelo = @Id ";

                
                    int nivelAnterior = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId)
                        .ExecuteQuery<int>(query, new Dictionary<string, object> { { "Id", modelosRequest.idModelo } });


                    if (nivelAnterior > 0)
                    {
                        tableName = "Parametros";

                        dataParam = new Dictionary<string, object>
                        {
                            { "estado",  0 },
                            { "usuarioModificacion", modelosRequest.usuarioModificacion },
                            { "fechaModificacion", modelosRequest.fechaModificacion  }
                        };

                        condition = " nroVersion=" + nivelAnterior;
                        IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                            UpdateData(tableName, dataParam, condition);

                        // Si retorna positivo, es decir se actualizo la version anterior
                        if (IdInsertada > 0)
                        {
                            foreach (var item in modelosRequest.Parametros)
                            {
                                dataParam = new Dictionary<string, object>
                                {
                                    { "idProyectos", modelosRequest.idProyectos },
                                    { "idModelo", modelosRequest.idModelo },
                                    { "parametro_cosapi", "'" + item.parametro_cosapi + "'"},
                                    { "grupo", "'" + item.grupo + "'"},
                                    { "parametro", "'" + item.parametro + "'"},
                                    { "valor", "'" + item.valor + "'"},
                                    { "estado", "'1'" },
                                    { "nroVersion", "'"+(nivelAnterior + 1)+"'"   },
                                    { "usuarioCreacion", modelosRequest.usuarioModificacion },
                                    { "fechaCreacion",  "'" + modelosRequest.fechaModificacion + "'"  }
                                };

                                idParametrizado = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                                    InsertData(tableName, dataParam);
                            }
                        }
                    }
                }

                result.code = 200000;
                result.message = "success";
                result.content = idParametrizado;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Actualizar_Modelos.", e);
            }
            return result;
        }

        /*
           public ObjectResult Actualizar_Modelos(ModelosRequest modelosRequest)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                int IdInsertada = 0;
                int idParametrizado = 0;
                var tableName = "Modelos";

                var dataParam = new Dictionary<string, object>
                    {
                        { "disciplina",  modelosRequest.disciplina },
                        { "usuarioModificacion", modelosRequest.usuarioModificacion },
                        { "fechaModificacion", modelosRequest.fechaModificacion  }
                    };

                var condition = " idModelo=" + modelosRequest.idModelo;
                IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, dataParam, condition);

                if (IdInsertada > 0)
                {
                    foreach (var item in modelosRequest.Parametros)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("IdProyecto", modelosRequest.idProyectos);
                        parameters.Add("IdModelo", modelosRequest.idModelo);
                        parameters.Add("IdParametroCosapi", item.parametro_cosapi);
                        parameters.Add("NombreGrupo", item.grupo);
                        parameters.Add("NombreParametro", item.parametro);

                        string query = @"
                            SELECT idParametros , valor
                            FROM Parametros
                            WHERE idProyectos = @IdProyecto AND idModelo = @IdModelo  
                            AND parametro_cosapi = @IdParametroCosapi
                            AND grupo = @NombreGrupo
                            AND parametro = @NombreParametro AND estado = 1";

                        List<Parametros> listaParametro = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                           Query<Parametros>(query, parameters);

                        tableName = "Parametros";

                        if (listaParametro.Count() > 0) // Existe Parametro
                        {
                            Parametros objParametro = listaParametro[0];

                            // Se evalua si el parametro es igual
                            if (objParametro.valor.Trim().Equals(item.valor.Trim()))
                            {
                                dataParam = new Dictionary<string, object>
                                {
                                    { "usuarioModificacion", modelosRequest.usuarioModificacion },
                                    { "fechaModificacion", modelosRequest.fechaModificacion  }
                                };

                                condition = " idParametros=" + objParametro.idParametros;

                                idParametrizado = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, dataParam, condition);

                            }
                            else
                            {
                                // Actualizar Parametro a Inactivo
                                dataParam = new Dictionary<string, object>
                                {
                                    { "estado",  0 },
                                    { "usuarioModificacion", modelosRequest.usuarioModificacion },
                                    { "fechaModificacion", modelosRequest.fechaModificacion  }
                                };

                                condition = " idParametros=" + objParametro.idParametros;

                                IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, dataParam, condition);

                                // Registrar Nuevo Parametro

                                if (IdInsertada > 0)
                                {
                                    dataParam = new Dictionary<string, object>
                                {
                                    { "idProyectos", modelosRequest.idProyectos },
                                    { "idModelo", modelosRequest.idModelo },
                                    { "parametro_cosapi", "'" + item.parametro_cosapi + "'"},
                                    { "grupo", "'" + item.grupo + "'"},
                                    { "parametro", "'" + item.parametro + "'"},
                                    { "valor", "'" + item.valor + "'"},
                                    { "estado", "'1'" },
                                    { "usuarioCreacion", modelosRequest.usuarioModificacion },
                                    { "fechaCreacion",  "'" + modelosRequest.fechaModificacion + "'"  }
                                };
                                    idParametrizado = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertData(tableName, dataParam);
                                }
                            } 
                        }
                        else
                        {
                                dataParam = new Dictionary<string, object>
                                {
                                    { "idProyectos", modelosRequest.idProyectos },
                                    { "idModelo", modelosRequest.idModelo },
                                    { "parametro_cosapi", "'" + item.parametro_cosapi + "'"},
                                    { "grupo", "'" + item.grupo + "'"},
                                    { "parametro", "'" + item.parametro + "'"},
                                    { "valor", "'" + item.valor + "'"},
                                    { "estado", "'1'" },
                                    { "usuarioCreacion", modelosRequest.usuarioModificacion },
                                    { "fechaCreacion",  "'" + modelosRequest.fechaModificacion + "'"  }
                                };
                                idParametrizado = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertData(tableName, dataParam);
                        }
                    }
                }

                result.code = 200000;
                result.message = "success";
                result.content = idParametrizado;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Actualizar_Modelos.", e);
            }
            return result;
        }
         */

        public List<ModeloResponse> Listar_ModeloPorProyecto(int idProyecto)
        {
            List<ModeloResponse> lista = new List<ModeloResponse> ();   
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdProyecto", idProyecto);

                string query = @"
                    SELECT idModelo, idProyectos, modelo, modeloversion, disciplina, fechaCreacion, usuarioCreacion
                    FROM (
                        SELECT idModelo, idProyectos, modelo, modeloversion, disciplina, m.fechaCreacion, u.usuario AS usuarioCreacion,
                               ROW_NUMBER() OVER (ORDER BY idModelo DESC) AS row_num
                        FROM Modelos m 
                        INNER JOIN Usuario u ON u.idUsuario = m.usuarioCreacion
                        WHERE idProyectos = @IdProyecto AND estado = 1
                    ) AS sub
                    WHERE row_num = 1";

                 lista = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                    Query<ModeloResponse>(query, parameters); 
            }
            catch (Exception e)
            {
                throw new Exception("Failed to Listar_Modelo.", e);
            }
            return lista;
        }
        public ModeloResponse Buscar_ModeloPorId(int idModelo)
        {
            ModeloResponse obj = null;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdModelo", idModelo);

                string query = @"
                    SELECT idModelo, idProyectos, modelo, modeloversion, disciplina, fechaCreacion, usuarioCreacion , urn
                    FROM Modelos
                    WHERE idModelo = @IdModelo AND estado = 1
                ";

                obj = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                   Query<ModeloResponse>(query, parameters).FirstOrDefault();

                if (obj != null)
                {
                    query = @"
                        SELECT idProyectos , idModelo,parametro_cosapi,grupo,parametro,valor
                        FROM Parametros
                        WHERE idModelo = @IdModelo AND estado = 1
                    ";

                    List<Parametros> listParametros = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                        Query<Parametros>(query, parameters);
                    obj.Parametros = listParametros;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to Buscar_ModeloPorId.", e);
            }
            return obj;
        }

        public ObjectResult EliminarModelo(int idModelo)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var tableName = "Modelos";
                var data = new Dictionary<string, object>
                {
                    { "estado", 0 }
                };
                var condition = " idModelo=" + idModelo;

                int filasActualizadas = this.databaseManager.
                    LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).
                    UpdateData(tableName, data, condition);

                result.code = 200000;
                result.message = "success";
                result.content = filasActualizadas;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to eliminar modelo.", e);
            }
            return result;
        }
    }
}
