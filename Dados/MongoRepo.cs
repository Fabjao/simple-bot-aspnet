using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SimpleBot.Dados
{
    public class MongoRepo : IUserRepo
    {
      

        public UserProfile BuscarPerfilId(string id)
        {
            var cliente = new MongoClient();
            var database = cliente.GetDatabase("Bot");
            var col = database.GetCollection<UserProfile>("perfil");

            var filtro = Builders<UserProfile>.Filter.Eq("Id", id);
            UserProfile user = col.Find(filtro).FirstOrDefault();
            if (user == null)
            {
                return new UserProfile()
                {
                    Id = id,
                    Visitas = 0
                };
            }
            return user;
        }
        
        public void SalvarHistorico(Message message)
        {
            var cliente = new MongoClient();
            var database = cliente.GetDatabase("Bot");
            var col = database.GetCollection<BsonDocument>("historico");

            var doc = new BsonDocument()
            {
                {"ID ", message.Id },
                {"Usuario",message.User },
                {"Mensagem",message.Text }
            };

            col.InsertOne(doc);
        }

        public void SalvarPerfil(UserProfile profile)
        {
            var cliente = new MongoClient();
            var database = cliente.GetDatabase("Bot");
            var col = database.GetCollection<UserProfile>("perfil");

            var filter = Builders<UserProfile>.Filter.Eq("Id", profile.Id);

            col.ReplaceOne(filter, profile, new UpdateOptions() { IsUpsert = true });
        }
    }
}