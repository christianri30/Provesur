namespace Provesur.Models.Response
{
    public class Respuesta
    {   
        public bool Resultado { get; set; } = false;
        public string CodigoError { get; set; }
        public object Data { get; set; }
        public bool ActualizarNotificacion { get; set; }
        public object DataNotificacion { get; set; }
        public bool Email { get; set; }
        public object Objeto { get; set; }
    }
}
