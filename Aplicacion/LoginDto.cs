namespace Microservicio.Login.Api.Aplicacion
{
    public class LoginDto
    {
        public string Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string LoginGuid { get; set; }

        // Nuevos campos
        public string PreguntaRecuperacion { get; set; }
        public string RespuestaRecuperacion { get; set; }
    }
}
