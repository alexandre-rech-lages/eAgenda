using GestaoTarefas.Dominio;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp.ModuloTarefas
{
    public partial class ListagemTarefasControl : UserControl
    {
        public ListagemTarefasControl()
        {
            InitializeComponent();
        }

        public void AtualizarRegistros(List<Tarefa> tarefasPendentes, List<Tarefa> tarefasConcluidas)
        {
            CarregarTarefasPendentes(tarefasPendentes);

            CarregarTarefasConcluidas(tarefasConcluidas);
        }

        public Tarefa ObtemTarefaSelecionada()
        {
            return (Tarefa)listTarefasPendentes.SelectedItem;
        }

        private void CarregarTarefasConcluidas(List<Tarefa> tarefasConcluidas)
        {
            listTarefasConcluidas.Items.Clear();

            foreach (Tarefa t in tarefasConcluidas)
            {
                listTarefasConcluidas.Items.Add(t);
            }
        }

        private void CarregarTarefasPendentes(List<Tarefa> tarefasPendentes)
        {
            listTarefasPendentes.Items.Clear();

            foreach (Tarefa t in tarefasPendentes)
            {
                listTarefasPendentes.Items.Add(t);
            }
        }
    }
}
