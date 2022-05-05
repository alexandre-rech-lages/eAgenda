using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestaoTarefas.Dominio
{
    [Serializable]
    public class Tarefa : EntidadeBase<Tarefa>
    {
        private List<ItemTarefa> itens = new List<ItemTarefa>();

        public Contato ContatoSelecionado { get; set; }


        public Tarefa()
        {
            DataCriacao = DateTime.Now;
        }

        public Tarefa(int n, string t) : this()
        {
            Numero = n;
            Titulo = t;
            DataConclusao = null;
        }

        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public List<ItemTarefa> Itens { get { return itens; } }

        public override string ToString()
        {
            var percentual = CalcularPercentualConcluido();

            if (DataConclusao.HasValue)
            {
                return $"Número: {Numero}, Título: {Titulo}, Percentual: {percentual}, " +
                    $"Concluída: {DataConclusao.Value.ToShortDateString()}";
            }

            return $"Número: {Numero}, Título: {Titulo}, Percentual: {percentual}";
        }

        public void AdicionarItem(ItemTarefa item)
        {
            if (Itens.Exists(x => x.Equals(item)) == false)
                itens.Add(item);
        }

        public void ConcluirItem(ItemTarefa item)
        {
            ItemTarefa itemTarefa = itens.Find(x => x.Equals(item));

            itemTarefa?.Concluir();

            var percentual = CalcularPercentualConcluido();

            if (percentual == 100)
                DataConclusao = DateTime.Now;
        }

        public void MarcarPendente(ItemTarefa item)
        {
            ItemTarefa itemTarefa = itens.Find(x => x.Equals(item));

            itemTarefa?.MarcarPendente();
        }

        public decimal CalcularPercentualConcluido()
        {
            if (itens.Count == 0)
                return 0;

            int qtdConcluidas = itens.Count(x => x.Concluido);

            var percentualConcluido = (qtdConcluidas / (decimal)itens.Count()) * 100;

            return Math.Round(percentualConcluido, 2);
        }

        public string Validar()
        {
            StringBuilder resultadoValidacao = new StringBuilder();

            if (string.IsNullOrEmpty(Titulo))
                resultadoValidacao.AppendLine("O campo Título é obrigatório");

            if (DataCriacao == DateTime.MinValue)
                resultadoValidacao.AppendLine("O campo Data de Criação é obrigatório");

            if (resultadoValidacao.Length == 0)
                resultadoValidacao.AppendLine("ESTA_VALIDO");

            return resultadoValidacao.ToString();
        }

        public override void Atualizar(Tarefa registro)
        {

        }
    }
}
