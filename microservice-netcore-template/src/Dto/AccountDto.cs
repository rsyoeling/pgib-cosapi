using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    //public class AccountDto
    //{

    //}
    public class LoginRequest
    {
        public string usuario { get; set; }
        public string clave { get; set; }
    }
    public class LoginResponse
    {
        public int idUsuario { get; set; }
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
    }

    public class MenuSubmenuResponse {
        public List<Menu> menu { get; set; }
        public List<Submenu> subMenu { get; set; }
    }
    public class Menu
    {
        public int idMenu { get; set; }
        public string menuNombre { get; set; }
    }
    public class Submenu
    {
        public int idMenu { get; set; }
        public string menuNombre { get; set; }
        public int idSubmenu { get; set; }
        public string submenuNombre { get; set; }
        public string urlPagina { get; set; }
    }
}
