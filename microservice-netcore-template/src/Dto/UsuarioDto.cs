using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class UsuarioRequest
    {
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string correoElectronico { get; set; }
        public byte status { get; set; }
    }
    public class Usuario {
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string nombresCompleto { get; set; }
        public string rolNombre { get; set; }
        public string correoElectronico { get; set; }
        public byte status { get; set; }
    }
    public class UsuarioId
    {
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string correoElectronico { get; set; }
    }
    public class ActUsuRequest
    {
        public int idUsuario { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int idRol { get; set; }
        public string correoElectronico { get; set; }
    }
}
