using MongoDB.Bson;
using SimpleBot.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();
        private static string tipoBanco = "mongoDB"; //"SqlServer";//
        public static string Reply(Message message)
        {
            string userId = message.Id;
            UserProfile perfil = null;
            if (tipoBanco.Equals("mongoDB"))
            {
                Mongo mongo = new Mongo();
                //  mongo.Inserir(message);
                perfil = mongo.BuscarPerfilId(userId);
                perfil.Visitas += 1;
                mongo.InserirPerfil(userId, perfil);
            }
            else
            {
                SqlServer sql = new SqlServer();
                perfil = sql.BuscarPerfilId(userId);
                if (perfil == null)
                {
                    perfil = new UserProfile
                    {
                        Id = userId,
                        Visitas = 1
                    };
                    sql.InserirPerfil(userId, perfil);
                }
                else
                {
                    perfil.Visitas += 1;
                    sql.AtualizarPerfil(perfil);
                }

            }

            return $"{perfil.Id} conversou {perfil.Visitas}";
            // return $"{message.User} disse '{message.Text}'";
        }


    }
}