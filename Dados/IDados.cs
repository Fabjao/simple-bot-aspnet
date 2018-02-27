using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Dados
{
    public interface IDados
    {
        void Inserir(Message message);
        void InserirPerfil(string id, UserProfile profile);
        UserProfile BuscarPerfilId(string id);
        void AtualizarPerfil(UserProfile profile);
    }
}
