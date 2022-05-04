using GestaoTarefas.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.WinApp.ModuloContatos
{
    internal class ControladorContato : ControladorBase
    {
        public override void Inserir()
        {
            TelaCadastroContatoForm tela = new TelaCadastroContatoForm();

            tela.ShowDialog();
        }

        public override void Editar()
        {
            TelaCadastroContatoForm tela = new TelaCadastroContatoForm();

            tela.ShowDialog();
        }        
        public override void Excluir()
        {
            System.Windows.Forms.MessageBox.Show("Não implmenentado");
        }
    }
}
