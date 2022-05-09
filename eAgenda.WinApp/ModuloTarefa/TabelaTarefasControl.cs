using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloTarefa
{
    public partial class TabelaTarefasControl : UserControl
    {
        public TabelaTarefasControl()
        {
            InitializeComponent();
            gridTarefas.ConfigurarGridZebrado();
            gridTarefas.ConfigurarGridSomenteLeitura();
            gridTarefas.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Titulo", HeaderText = "Título"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Prioridade", HeaderText = "Prioridade"},

                new DataGridViewTextBoxColumn { DataPropertyName = "DataCriacao", HeaderText = "Criada em"},

                new DataGridViewTextBoxColumn { DataPropertyName = "DataConclusao", HeaderText = "Concluída em"},

                new DataGridViewTextBoxColumn { DataPropertyName = "PercentualConcluido", HeaderText = "% Concluído"},
            };

            return colunas;
        }

        public int ObtemNumeroTarefaSelecionado()
        {
            return gridTarefas.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Tarefa> tarefas)
        {
            gridTarefas.Rows.Clear();

            foreach (Tarefa tarefa in tarefas)
            {
                gridTarefas.Rows.Add(tarefa.Numero, tarefa.Titulo, tarefa.Prioridade,
                    tarefa.DataCriacao, tarefa.DataConclusao, tarefa.PercentualConcluido);
            }
        }


    }
}
