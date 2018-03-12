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
        //static IUserRepo _dados = new MongoRepo();
        static IUserRepo _dados = new SqlServerRepo(ConfigurationManager.AppSettings["stringConexao"].ToString());

        public static string Reply(Message message)
        {
            string userId = message.Id;

            UserProfile perfil = null;
            perfil = _dados.BuscarPerfilId(userId);

            perfil.Visitas += 1;
            
            _dados.SalvarPerfil(perfil);
            _dados.SalvarHistorico(message);

            return $"{perfil.Id} conversou {perfil.Visitas}";
        }


    }
}