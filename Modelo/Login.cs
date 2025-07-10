using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservicio.Login.Api.Modelo
{
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        // Puedes agregar un GUID único para seguimiento (igual que tu AutorLibroGuid)
        [BsonElement("loginGuid")]
        public string LoginGuid { get; set; }
        
        // Nuevos campos
        [BsonElement("preguntaRecuperacion")]
        public string PreguntaRecuperacion { get; set; }

        [BsonElement("respuestaRecuperacion")]
        public string RespuestaRecuperacion { get; set; }
    }
}
