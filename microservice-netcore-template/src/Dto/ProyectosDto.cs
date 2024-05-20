using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class ProyectosDto
    {
        public int idProyectos { get; set; }
        public string cr { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public string usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaModificacion { get; set; }
    }
    public class ProyectosRequest
    {
        public string cr { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public int usuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
    }
}
