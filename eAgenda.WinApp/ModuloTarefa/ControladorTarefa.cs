using eAgenda.Dominio.ModuloTarefa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloTarefa
{
    public class ControladorTarefa : ControladorBase
    {
        private IRepositorioTarefa repositorioTarefa;
        private ListagemTarefasControl listagemTarefas;

        public ControladorTarefa(IRepositorioTarefa repositorioTarefa)
        {
            this.repositorioTarefa = repositorioTarefa;
        }

        public override void Inserir()
        {
            TelaCadastroTarefasForm tela = new TelaCadastroTarefasForm();
            tela.Tarefa = new Tarefa();

            tela.GravarRegistro = repositorioTarefa.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                //repositorioTarefa.Inserir(tela.Tarefa);

                CarregarTarefas();
            }
        }

        

        public override void Editar()
        {
            Tarefa tarefaSelecionada = listagemTarefas.ObtemTarefaSelecionada();

            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Selecione uma tarefa primeiro",
                "Edição de Tarefas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTarefasForm tela = new TelaCadastroTarefasForm();

            tela.Tarefa = tarefaSelecionada;

            tela.GravarRegistro = repositorioTarefa.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTarefas();
            }
        }
        public override void Excluir()
        {
            Tarefa tarefaSelecionada = listagemTarefas.ObtemTarefaSelecionada();

            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Selecione uma tarefa primeiro",
                "Exclusão de Tarefas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a tarefa?",
                "Exclusão de Tarefas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTarefa.Excluir(tarefaSelecionada);
                CarregarTarefas();
            }
        }

        public override void AdicionarItens()
        {
            Tarefa tarefaSelecionada = listagemTarefas.ObtemTarefaSelecionada();

            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Selecione uma tarefa primeiro",
                "Edição de Tarefas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroItensTarefaForm tela = new TelaCadastroItensTarefaForm(tarefaSelecionada);

            if (tela.ShowDialog() == DialogResult.OK)
            {
                List<ItemTarefa> itens = tela.ItensAdicionados;

                repositorioTarefa.AdicionarItens(tarefaSelecionada, itens);

                CarregarTarefas();
            }
        }

        public override void AtualizarItens()
        {
            Tarefa tarefaSelecionada = listagemTarefas.ObtemTarefaSelecionada();

            if (tarefaSelecionada == null)
            {
                MessageBox.Show("Selecione uma tarefa primeiro",
                "Edição de Tarefas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaAtualizacaoItensTarefaForm tela = new TelaAtualizacaoItensTarefaForm(tarefaSelecionada);

            if (tela.ShowDialog() == DialogResult.OK)
            {
                List<ItemTarefa> itensConcluidos = tela.ItensConcluidos;

                List<ItemTarefa> itensPendentes = tela.ItensPendentes;

                repositorioTarefa.AtualizarItens(tarefaSelecionada, itensConcluidos, itensPendentes);
                CarregarTarefas();
            }
        }

        public override UserControl ObtemListagem()
        {
            if (listagemTarefas == null)
                listagemTarefas = new ListagemTarefasControl(repositorioTarefa);

            CarregarTarefas();

            return listagemTarefas;
        }

        private void CarregarTarefas()
        {
            var tarefasPendentes = repositorioTarefa.SelecionarTarefasPendentes();

            var tarefasConcluidas = repositorioTarefa.SelecionarTarefasConcluidas();

            listagemTarefas.AtualizarRegistros(tarefasPendentes, tarefasConcluidas);
        }

        internal override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxTarefa();
        }
    }
}
