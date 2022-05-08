using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloTarefa
{
    public partial class TelaCadastroTarefasForm : Form // View
    {
        private Tarefa tarefa;

        public TelaCadastroTarefasForm()
        {
            InitializeComponent();
        }

        public Func<Tarefa, string> OperacaoGravar { get; set; }    

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

            string resultado = tarefa.Validar();                                

            if (resultado == "ESTA_VALIDO")
            {
                resultado = OperacaoGravar(tarefa);

                if (resultado != "ESTA_VALIDO")
                {
                    DialogResult = DialogResult.None;
                }
            }
        }
    }
}
