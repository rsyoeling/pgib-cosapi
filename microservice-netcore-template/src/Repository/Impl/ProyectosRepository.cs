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
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.Impl
{
    [Service(Scope = "Transient")]
    public class ProyectosRepository : IProyectosRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<ProyectosRepository> logger;
        private readonly DatabaseManager databaseManager;

        public ProyectosRepository(IConfiguration configuration, ILogger<ProyectosRepository> logger, DatabaseManager databaseManager) {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public ObjectResult ListarProyectos(int idUser)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("idUser", idUser);

                List<ProyectosDto> rpt = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<ProyectosDto>("select py.idProyectos,py.cr,py.nombre,py.imagen,py.usuarioCreacion,py.fechaCreacion,py.fechaModificacion from Proyectos py" +
                " join AccesoProyectos ap on(ap.idProyectos = py.idProyectos) join Usuario us on(us.idUsuario = py.usuarioCreacion)" +
                " where py.estado = 1 and ap.acceso = 1 and ap.idUsuario = @idUser", dynamicParameters);

                result.code = 200000;
                result.message = "success";
                result.content = rpt;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Login.", e);
            }
            return result;
        }

        public ObjectResult Insertar_Proyectos(ProyectosRequest proyectosRequest)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                int idAsignado=0;
                var tableName = "Proyectos";
                var data = new Dictionary<string, object>
                {
                    { "cr", "'" + proyectosRequest.cr + "'" },
                    { "nombre", "'" + proyectosRequest.nombre + "'" },
                    { "descripcion", "'" + proyectosRequest.descripcion + "'"},
                    { "estado", "'1'" },
                    { "imagen", "'" + proyectosRequest.imagen + "'" },
                    { "usuarioCreacion", proyectosRequest.usuarioCreacion },
                    { "fechaCreacion", "GETDATE()" }
                };

                int IdInsertada = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertDataId(tableName, data);

                if (IdInsertada > 0) {
                    var tableAcceso = "AccesoProyectos";
                    var dataAcceso = new Dictionary<string, object>
                    {
                        { "idUsuario", proyectosRequest.usuarioCreacion },
                        { "idProyectos", IdInsertada },
                        { "acceso", "'1'"},
                        { "usuarioCreacion", proyectosRequest.usuarioCreacion },
                        { "fechaCreacion", "GETDATE()" }
                    };

                    idAsignado = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertData(tableAcceso, dataAcceso);
                }

                result.code = 200000;
                result.message = "success";
                result.content = idAsignado;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Insertar_Usuario.", e);
            }
            return result;
        }
    }
}
