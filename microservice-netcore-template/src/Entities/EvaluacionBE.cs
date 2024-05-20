namespace Api.Entities
{
    public class EvaluacionBE
    {
        public int CEvaluacion { get; set; }
        public string DEvaluacion { get; set; }
        public string SEvaluacion { get; set; }
    }

    //YR
    public class UsuarioBE {
        public int idUsuario { get; set; }
        public int idRol { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
    }
}
