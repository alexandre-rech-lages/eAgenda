using GestaoTarefas.Dominio;
using System;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp.ModuloTarefas
{
    public partial class TelaCadastroTarefasForm : Form // View
    {
        private Tarefa tarefa;

        public TelaCadastroTarefasForm()
        {
            InitializeComponent();
        }

        public Tarefa Tarefa
        {
            get
            {
                return tarefa;
            }
            set
            {
                tarefa = value;
                txtNumero.Text = tarefa.Numero.ToString();
                txtTitulo.Text = tarefa.Titulo;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            tarefa.Titulo = txtTitulo.Text;
        }
    }
}
