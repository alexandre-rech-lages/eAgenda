using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Dominio
{
    public interface IRepositorioContato
    {
        void Inserir(Contato contato);
    }
}
