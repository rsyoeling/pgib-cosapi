using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IAccountRepository
    {
        ObjectResult Login(LoginRequest usuarioRequest);
        ObjectResult MenuSubmenu(int idRol);
    }
}
