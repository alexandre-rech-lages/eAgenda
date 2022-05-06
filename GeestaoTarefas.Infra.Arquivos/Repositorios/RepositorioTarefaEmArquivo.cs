using eAgenda.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos
{
    public class RepositorioTarefaEmArquivo : RepositorioEmArquivoBase<Tarefa>, IRepositorioTarefa
    {
        public RepositorioTarefaEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Tarefas.Count > 0)
                contador = dataContext.Tarefas.Max(x => x.Numero);
        }

        public override string Inserir(Tarefa novoRegistro)
        {
            var nomeEncontrado = ObterRegistros()
                .Select(x => x.Titulo)
                .Contains(novoRegistro.Titulo);

            if (nomeEncontrado)
                return "Nome já está cadastrado";

            novoRegistro.Numero = ++contador;

            var registros = ObterRegistros();

            registros.Add(novoRegistro);

            return "ESTA_VALIDO";
        } 

        public override List<Tarefa> ObterRegistros()
        {
            return dataContext.Tarefas;
        }

        public void AdicionarItens(Tarefa tarefaSelecionada, List<ItemTarefa> itens)
        {
            foreach (var item in itens)
            {
                tarefaSelecionada.AdicionarItem(item);
            }
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
