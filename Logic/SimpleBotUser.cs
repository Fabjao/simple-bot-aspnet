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
        static IDados _dados = new Mongo();
        //private static string tipoBanco = "mongoDB"; //"SqlServer";//
        public static string Reply(Message message)
        {
            string userId = message.Id;

            UserProfile perfil = null;
            Mongo mongo = new Mongo();;
            perfil = mongo.BuscarPerfilId(userId);

           // SqlServer sql = new SqlServer(ConfigurationManager.AppSettings["stringConexao"].ToString());
          //  perfil = sql.BuscarPerfilId(userId);

            perfil.Visitas += 1;

            mongo.InserirPerfil(userId, perfil);
            //sql.InserirPerfil(userId, perfil);

            return $"{perfil.Id} conversou {perfil.Visitas}";
            // return $"{message.User} disse '{message.Text}'";
        }


    }
}