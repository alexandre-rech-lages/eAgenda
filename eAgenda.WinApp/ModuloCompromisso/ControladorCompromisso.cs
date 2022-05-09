using eAgenda.WinApp.Compartilhado;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloCompromisso
{
    public class ControladorCompromisso : ControladorBase
    {
        public override void Editar()
        {
            MessageBox.Show("Test");
        }

        public override void Excluir()
        {
            MessageBox.Show("Test");
        }

        public override void Inserir()
        {
            MessageBox.Show("Test");
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxCompromisso();
        }

        public override UserControl ObtemListagem()
        {
            return new UserControl();
        }
    }
}
