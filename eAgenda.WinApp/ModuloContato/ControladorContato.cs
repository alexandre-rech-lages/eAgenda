using eAgenda.Dominio.ModuloContato;
using eAgenda.WinApp.Compartilhado;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloContato
{
    internal class ControladorContato : ControladorBase
    {
        private ListagemContatosControl listagemContatos;

        public ControladorContato(IRepositorioContato repositorioContato)
        {

        }

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

        public override UserControl ObtemListagem()
        {
            if (listagemContatos == null)
                listagemContatos = new ListagemContatosControl();

            return listagemContatos;
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxContato();
        }
    }
}
