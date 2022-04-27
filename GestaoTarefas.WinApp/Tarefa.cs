using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoTarefas.WinApp
{
    public class Tarefa
    {
        private List<ItemTarefa> itens = new List<ItemTarefa>();

        public Tarefa()
        {
            DataCriacao = DateTime.Now;
        }

        public Tarefa(int n, string t) : this()
        {
            Numero = n;
            Titulo = t;
        }

        public int Numero { get; set; }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItemTarefa> Itens { get { return itens; } }

        public override string ToString()
        {
            var percentual = CalcularPercentualConcluido();

            return $"Número: {Numero}, Título: {Titulo}, Percentual: {percentual} ";
        }

        public void AdicionarItem(ItemTarefa item)
        {
            if (Itens.Exists(x => x.Equals(item)) == false)
                itens.Add(item);
        }

        public void ConcluirItem(ItemTarefa item)
        {

            ItemTarefa itemTarefa = itens.Find(x => x.Equals(item));

            if (itemTarefa != null)
                itemTarefa.Concluir();

        }

        public void MarcarPendente(ItemTarefa item)
        {
            ItemTarefa itemTarefa = itens.Find(x => x.Equals(item));

            itemTarefa.MarcarPendente();
        }

        public decimal CalcularPercentualConcluido()
        {
            if (itens.Count == 0)
                return 0;

            int qtdConcluidas = itens.Count(x => x.Concluido);

            var percentualConcluido = (qtdConcluidas / (decimal)itens.Count()) * 100;

            return Math.Round(percentualConcluido, 2);
        }


    }
}
