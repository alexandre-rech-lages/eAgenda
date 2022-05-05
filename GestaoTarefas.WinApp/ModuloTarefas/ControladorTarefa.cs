using GestaoTarefas.Dominio;
using GestaoTarefas.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp.ModuloTarefas
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

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                repositorioTarefa.Inserir(tela.Tarefa);

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

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                repositorioTarefa.Editar(tela.Tarefa);
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
                listagemTarefas = new ListagemTarefasControl();

            return listagemTarefas;
        }

        private void CarregarTarefas()
        {
            var tarefasPendentes = repositorioTarefa.SelecionarTarefasPendentes();

            var tarefasConcluidas = repositorioTarefa.SelecionarTarefasConcluidas();

            listagemTarefas.AtualizarRegistros(tarefasPendentes, tarefasConcluidas);
        }
    }
}
