using MongoDB.Driver;
using Microservicio.Login.Api.Modelo;

namespace Microservicio.Login.Api.Persistencia
{
    public class ContextoMongo
    {
        private readonly IMongoDatabase _database;

        public ContextoMongo(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Modelo.Usuarioss> UsuarioCollection =>
            _database.GetCollection<Modelo.Usuarioss>("Usuario");
    }
}
