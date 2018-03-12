using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Dados
{
    public interface IUserRepo
    {
        void SalvarHistorico(Message message);        
        UserProfile BuscarPerfilId(string id);
        void AtualizarPerfil(UserProfile profile);
    }
}
