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

        public static string Reply(Message message)
        {
            //Mongo mongo = new Mongo();
            //mongo.Inserir(message);
            string userId = message.Id;

            var perfil = GetProfile(userId);
            perfil.Visitas += 1;
            SetProfile(userId, perfil);


            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            if (_perfil.ContainsKey(id))
                return _perfil[id];

            return new UserProfile
            {
                Id = id,
                Visitas = 0
            };
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            _perfil[id] = profile;
        }
    }
}