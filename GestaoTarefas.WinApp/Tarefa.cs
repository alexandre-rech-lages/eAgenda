using System;
using System.Collections.Generic;

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
            return $"Número: {Numero}, Título: {Titulo}";
        }

        public void AdicionarItem(ItemTarefa item)
        {
            if (Itens.Exists(x => x.Equals(item)) == false)
                itens.Add(item);
        }
    }
}
