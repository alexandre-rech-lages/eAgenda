using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public partial class TabelaCategoriasDespesaControl : UserControl
    {
        public TabelaCategoriasDespesaControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        private DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
           {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Descricao", HeaderText = "Descrição"}

           };

            return colunas;
        }

        internal void AtualizarRegistros(List<CategoriaDespesa> categorias)
        {
            grid.Rows.Clear();

            foreach (var categoria in categorias)
            {
                grid.Rows.Add(categoria.Numero, categoria.Titulo);
            }
        }

        internal int ObtemNumeroCategoriaDespesaSelecionada()
        {
            return grid.SelecionarNumero<int>();
        }
    }
}
