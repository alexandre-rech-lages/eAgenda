using eAgenda.Dominio.ModuloTarefa;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloTarefa
{
    public partial class ListagemTarefasControl : UserControl
    {
        private readonly IRepositorioTarefa repositorioTarefa;

        public ListagemTarefasControl(IRepositorioTarefa repositorioTarefa)
        {
            InitializeComponent();
            this.repositorioTarefa = repositorioTarefa;
        }

        public void AtualizarRegistros(List<Tarefa> tarefasPendentes, List<Tarefa> tarefasConcluidas)
        {
            CarregarTarefasPendentes(tarefasPendentes);

            CarregarTarefasConcluidas(tarefasConcluidas);
        }

        public Tarefa ObtemTarefaSelecionada()
        {
            return (Tarefa)repositorioTarefa.SelecionarPorNumero();
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
