using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SimpleBot.Dados
{
    public class Mongo
    {
        

        public void Inserir(Message message)
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

        public void InserirPerfil(string id,UserProfile profile)
        {
            var cliente = new MongoClient();
            var database = cliente.GetDatabase("Bot");
            var col = database.GetCollection<BsonDocument>("perfil");

            var doc = new BsonDocument()
            {
                {"Id",id },
                {"Visitas", profile.Visitas }
            };
            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
            UpdateOptions options = new UpdateOptions();
            options.IsUpsert = true;
            col.ReplaceOne(filter,doc, options );
        }

        public UserProfile BuscarPerfilId(string id)
        {
            var cliente = new MongoClient();
            var database = cliente.GetDatabase("Bot");
            var filtro = Builders<BsonDocument>.Filter.Eq("Id", id);
            var col = database.GetCollection<BsonDocument>("perfil");
            BsonDocument user = col.Find(filtro).FirstOrDefault();

            return new UserProfile()
            {
                Id = user.GetValue("Id").ToString(),
                Visitas =user.GetValue("Visitas").ToInt32()
            };
        }




    }
}