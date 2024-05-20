using Api.Dto;
using Api.Entities;
using Api.Repository;
using Common.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Impl
{
    [Service(Scope = "Transient")]
    public class AccountService : IAccountService
    {
        private readonly IConfiguration config;
        private readonly IAccountRepository AdministracionRepository;
        private readonly ILogger<AccountService> logger;

        public AccountService(IConfiguration config, IAccountRepository AdministracionRepository,
            ILogger<AccountService> logger)
        {
            this.config = config;
            this.AdministracionRepository = AdministracionRepository;
            this.logger = logger;
        }

        public ObjectResult Login(LoginRequest usuarioRequest)
        {
            try
            {
                return this.AdministracionRepository.Login(usuarioRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch Login.", ex);
            }
        }
        public ObjectResult MenuSubmenu(int idRol)
        {
            try
            {
                return this.AdministracionRepository.MenuSubmenu(idRol);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch MenuSubmenu.", ex);
            }
        }
    }
}
