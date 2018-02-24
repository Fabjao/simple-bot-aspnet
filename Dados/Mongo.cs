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

        public void Inserir(Message message) {
            var cliente = new MongoClient();
            var db = cliente.GetDatabase("Bot");
            var col = db.GetCollection<BsonDocument>("historico");


            var doc = new BsonDocument()
            {
                {"ID ", message.Id },
                {"Usuario",message.User },
                {"Mensagem",message.Text }
            };

            col.InsertOne(doc);
        }
    }
}