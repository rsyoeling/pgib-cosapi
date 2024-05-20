using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Impl
{
    public interface IAccountService
    {
        ObjectResult Login(LoginRequest loginRequest);
        ObjectResult MenuSubmenu(int idRol);
    }
}
