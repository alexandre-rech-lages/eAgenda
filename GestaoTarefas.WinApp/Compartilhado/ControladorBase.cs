using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.WinApp.Compartilhado
{
    public abstract class ControladorBase
    {
        public abstract void Inserir();
        public abstract void Editar();
        public abstract void Excluir();

        public virtual void AdicionarItens() { }

        public virtual void AtualizarItens() { }
    }
}
