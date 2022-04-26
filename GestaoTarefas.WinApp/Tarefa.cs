using System;

namespace GestaoTarefas.WinApp
{
    public class Tarefa
    {
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

        public override string ToString()
        {
            return $"Número: {Numero}, Título: {Titulo}";
        }
    }
}
