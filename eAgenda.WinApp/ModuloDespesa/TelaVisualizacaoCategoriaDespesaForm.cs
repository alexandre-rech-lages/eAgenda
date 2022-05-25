using eAgenda.Dominio.ModuloDespesa;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public partial class TelaVisualizacaoCategoriaDespesaForm : Form
    {
        public TelaVisualizacaoCategoriaDespesaForm(CategoriaDespesa categoriaDespesaSelecionada)
        {
            InitializeComponent();
            labelTituloTarefa.Text = categoriaDespesaSelecionada.Titulo;

            foreach (var item in categoriaDespesaSelecionada.Despesas)
            {
                listDespesas.Items.Add(item);
            }
        }
    }
}
