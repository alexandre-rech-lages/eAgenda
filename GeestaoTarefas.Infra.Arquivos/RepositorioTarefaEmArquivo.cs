using GeestaoTarefas.Infra.Arquivos;
using GestaoTarefas.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace GestaoTarefas.Infra.Arquivos
{
    public class RepositorioTarefaEmArquivo : IRepositorioTarefa
    {

        private readonly ISerializador serializador;
        private readonly DataContext dataContext;
        //List<Tarefa> tarefas;
        private int contador = 0;


        public RepositorioTarefaEmArquivo(ISerializador serializador, DataContext dataContext)
        {
            this.serializador = serializador;
            this.dataContext = dataContext;

            dataContext.Tarefas.AddRange( serializador.CarregarDadosDoArquivo().Tarefas);

            //if (tarefas.Count > 0)
            //    contador = tarefas.Max(x => x.Numero);            
        }

        public List<Tarefa> SelecionarTodos()
        {
            return dataContext.Tarefas;
        }

        public void Inserir(Tarefa novaTarefa)
        {
            novaTarefa.Numero = ++contador;
            dataContext.Tarefas.Add(novaTarefa);

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public void Editar(Tarefa tarefa)
        {
            foreach (var item in dataContext.Tarefas)
            {
                if (item.Numero == tarefa.Numero)
                {
                    item.Titulo = tarefa.Titulo;
                    break;
                }
            }

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public void Excluir(Tarefa tarefa)
        {
            dataContext.Tarefas.Remove(tarefa);

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public void AdicionarItens(Tarefa tarefaSelecionada, List<ItemTarefa> itens)
        {
            foreach (var item in itens)
            {
                tarefaSelecionada.AdicionarItem(item);
            }

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public void AtualizarItens(Tarefa tarefaSelecionada,
            List<ItemTarefa> itensConcluidos, List<ItemTarefa> itensPendentes)
        {
            foreach (var item in itensConcluidos)
            {
                tarefaSelecionada.ConcluirItem(item);
            }

            foreach (var item in itensPendentes)
            {
                tarefaSelecionada.MarcarPendente(item);
            }

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public List<Tarefa> SelecionarTarefasConcluidas()
        {
            return dataContext.Tarefas.Where(x => x.CalcularPercentualConcluido() == 100).ToList();
        }

        public List<Tarefa> SelecionarTarefasPendentes()
        {
            return dataContext.Tarefas.Where(x => x.CalcularPercentualConcluido() < 100).ToList();
        }
    }
}
