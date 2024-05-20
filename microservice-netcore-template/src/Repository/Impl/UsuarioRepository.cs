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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<UsuarioRepository> logger;
        private readonly DatabaseManager databaseManager;

        public UsuarioRepository(IConfiguration configuration, ILogger<UsuarioRepository> logger, DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }
        public ObjectResult Listar_Usuario()
        {
            ObjectResult result = new ObjectResult();
            try
            {
                //var dynamicParameters = new DynamicParameters();
                
                List<Usuario> rpt = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<Usuario>("select USU.idUsuario, USU.usuario, USU.nombres + ' ' +  USU.ApellidoPaterno + ' ' + USU.ApellidoMaterno as nombresCompleto, " +
                    " ROL.rolNombre, USU.correoElectronico, USU.[status]" +
                    " from [dbo].[Usuario] USU join ROL ROL on (USU.idRol=ROL.idRol) order by idUsuario", null); //dynamicParameters

                result.code = 200000;
                result.message = "success";
                result.content = rpt;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Listar_Usuario.", e);
            }
            return result;
        }
        public ObjectResult Listar_Usuario_Por_Id(int idUsuario)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("idUsuario", idUsuario);

                List<UsuarioId> rpt = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<UsuarioId>("select idRol,nombres,apellidoPaterno,apellidoMaterno,usuario,clave,correoElectronico " +
                    "from [dbo].[Usuario] where idUsuario=@idUsuario", dynamicParameters);

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
        public ObjectResult Insertar_Usuario(UsuarioRequest usuarioRequest)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var tableName = "Usuario";
                var data = new Dictionary<string, object>
                {
                    { "idRol", usuarioRequest.idRol },
                    { "nombres", "'" + usuarioRequest.nombres + "'" },
                    { "apellidoPaterno", "'" + usuarioRequest.apellidoPaterno + "'"},
                    { "apellidoMaterno", "'" + usuarioRequest.apellidoMaterno + "'" },
                    { "usuario", "'" + usuarioRequest.usuario + "'" },
                    { "clave", "'" + usuarioRequest.clave + "'" },
                    { "correoElectronico", "'" + usuarioRequest.correoElectronico + "'" },
                    { "status", usuarioRequest.status }
                };
                
                // Llamar al método UpdateData para actualizar los datos en la tabla
                int filasActualizadas = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).InsertData(tableName, data);

                result.code = 200000;
                result.message = "success";
                result.content = filasActualizadas;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Insertar_Usuario.", e);
            }
            return result;
        }
        public ObjectResult Actualizar_Usuario(ActUsuRequest usuarioRequest) {
            ObjectResult result = new ObjectResult();
            try
            {
                var tableName = "Usuario";
                var data = new Dictionary<string, object>
                {
                    { "idRol", usuarioRequest.idRol },
                    { "nombres", usuarioRequest.nombres },
                    { "apellidoPaterno", usuarioRequest.apellidoPaterno },
                    { "apellidoMaterno", usuarioRequest.apellidoMaterno },
                    { "correoElectronico", usuarioRequest.correoElectronico }
                    // Agrega más columnas y valores según sea necesario
                };
                var condition = " idUsuario=" + usuarioRequest.idUsuario;

                // Llamar al método UpdateData para actualizar los datos en la tabla
                int filasActualizadas = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, data, condition);

                result.code = 200000;
                result.message = "success";
                result.content = filasActualizadas;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Login.", e);
            }
            return result;
        }
        public ObjectResult Eliminar_Usuario(int idUsuario, byte estatus)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var tableName = "Usuario";
                var data = new Dictionary<string, object>
                {
                    { "status", estatus }
                    // Agrega más columnas y valores según sea necesario
                };
                var condition = " idUsuario=" + idUsuario;

                // Llamar al método UpdateData para actualizar los datos en la tabla
                int filasActualizadas = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).UpdateData(tableName, data, condition);

                result.code = 200000;
                result.message = "success";
                result.content = filasActualizadas;

            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch Login.", e);
            }
            return result;
        }
    }
}
