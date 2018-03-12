using MongoDB.Bson;
using SimpleBot.Dados;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();
        static IUserRepo _dados = new MongoRepo();
        //private static string tipoBanco = "mongoDB"; //"SqlServer";//
        public static string Reply(Message message)
        {
            string userId = message.Id;

            UserProfile perfil = null;
            //MongoRepo mongo = new MongoRepo();;
            //perfil = mongo.BuscarPerfilId(userId);

            SqlServerRepo sql = new SqlServerRepo(ConfigurationManager.AppSettings["stringConexao"].ToString());
            perfil = sql.BuscarPerfilId(userId);

            perfil.Visitas += 1;

            //mongo.InserirPerfil(perfil);
            //mongo.SalvarHistorico(message);


            sql.InserirPerfil(perfil);
            //sql.SalvarHistorico(message);

            return $"{perfil.Id} conversou {perfil.Visitas}";
            // return $"{message.User} disse '{message.Text}'";
        }


    }
}