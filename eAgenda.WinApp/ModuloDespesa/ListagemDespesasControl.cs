using eAgenda.Dominio.ModuloDespesa;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public partial class ListagemDespesasControl : UserControl
    {
        public ListagemDespesasControl()
        {
            InitializeComponent();
        }

        internal void AtualizarRegistros(List<Despesa> despesas)
        {
            listDespesas.Items.Clear();

            foreach (Despesa despesa in despesas)
            {
                listDespesas.Items.Add(despesa);
            }
        }

        internal Despesa SelecionarDespesa()
        {
            return (Despesa)listDespesas.SelectedItem;
        }
    }
}
