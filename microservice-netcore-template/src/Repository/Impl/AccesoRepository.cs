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
    public class AccesoRepository : IAccesoRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<AccesoRepository> logger;
        private readonly DatabaseManager databaseManager;

        public AccesoRepository(IConfiguration configuration, ILogger<AccesoRepository> logger, DatabaseManager databaseManager) {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }
        public ObjectResult Listar_AccesoPorRol(int idRol)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("idRol", idRol);

                List<Acceso> rpt = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<Acceso>("SELECT M.idMenu,M.menuNombre,S.idSubmenu,S.submenuNombre,ISNULL(A.acceso, 0) AS acceso" +
                    " FROM Menu M INNER JOIN Submenu S ON M.idMenu = S.idMenu" +
                    " LEFT JOIN Acceso A ON A.idMenu = M.idMenu AND A.idSubmenu = S.idSubmenu AND A.idRol = @idRol" +
                    " ORDER BY M.menuNombre, submenuNombre", dynamicParameters);

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
        public ObjectResult Actualizar_AccesoPorRol(AccesoRequest accesoRequest)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var tableName = "Acceso";
                var data = new Dictionary<string, object>
                {
                    { "acceso", accesoRequest.acceso },
                    // Agrega más columnas y valores según sea necesario
                };
                var condition = " idRol="+ accesoRequest.idRol + " and idSubmenu="+ accesoRequest.idSubmenu + " and idMenu="+ accesoRequest.idMenu;

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
