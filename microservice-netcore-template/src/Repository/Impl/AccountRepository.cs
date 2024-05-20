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
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<AccountRepository> logger;
        private readonly DatabaseManager databaseManager;

        public AccountRepository(IConfiguration configuration, ILogger<AccountRepository> logger, DatabaseManager databaseManager) {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }
        
        public ObjectResult Login(LoginRequest loginRequest) {
            ObjectResult result = new ObjectResult();          
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ParamUser", loginRequest.usuario);
                dynamicParameters.Add("ParamPswd", loginRequest.clave);

                List<LoginResponse>  rpt = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<LoginResponse>("SELECT" +
                    " dbo.Usuario.idUsuario, dbo.Usuario.idRol," +
                    " dbo.Usuario.nombres, dbo.Usuario.apellidoPaterno, dbo.Usuario.apellidoMaterno, dbo.Usuario.usuario" +
                    " FROM         dbo.Usuario" +
                    " join dbo.Rol on (dbo.Usuario.idRol=dbo.Rol.idRol)" +
                    " WHERE" +
                    " usuario=@ParamUser and clave=@ParamPswd", dynamicParameters);

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

        public ObjectResult MenuSubmenu(int idRol)
        {
            ObjectResult result = new ObjectResult();
            try
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("idRol", idRol);

                MenuSubmenuResponse rpt=new MenuSubmenuResponse();

                List <Menu> rptMenu = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<Menu>("SELECT M.idMenu, M.menuNombre FROM Menu M join Acceso A on" +
                    " A.idMenu=M.idMenu AND A.idRol=@idRol AND a.acceso=1" +
                    " where M.[status]=1" +
                    " group by M.idMenu, M.menuNombre, M.orderBy order by M.orderBy", dynamicParameters);

                List<Submenu> rptSubmenu = this.databaseManager.LookupDatabaseConnectorById(ApiConstants.osilDatabaseId).Query<Submenu>("SELECT M.idMenu, M.menuNombre, S.idSubmenu, S.submenuNombre, S.urlPagina" +
                    " FROM  Menu AS M join Submenu AS S ON M.idMenu = S.idMenu JOIN Acceso A on" +
                    " A.idMenu=M.idMenu AND A.idSubmenu=S.idSubmenu AND A.idRol=@idRol AND A.acceso=1" +
                    " WHERE M.[status]=1 and S.[status]=1 order  by M.idMenu,S.orderBy", dynamicParameters);
                rpt.menu = rptMenu;
                rpt.subMenu = rptSubmenu;

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
    }
}
