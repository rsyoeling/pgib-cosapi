using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class Acceso
    {
        public int idMenu { get; set; }
        public string menuNombre { get; set; }
        public int idSubmenu { get; set; }
        public string submenuNombre { get; set; }
        public string acceso { get; set; }
    }
    public class AccesoRequest {
        public int idRol { get; set; }
        public int idMenu { get; set; }
        public int idSubmenu { get; set; }
        public string acceso { get; set; }
    }
}
