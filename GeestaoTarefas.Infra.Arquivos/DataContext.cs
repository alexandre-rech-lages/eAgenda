using GestaoTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeestaoTarefas.Infra.Arquivos
{
    [Serializable]
    public class DataContext
    {
        public DataContext()
        {
            Tarefas = new List<Tarefa>();

            Contatos = new List<Contato>();
        }

        public List<Tarefa> Tarefas { get; set; }

        public List<Contato> Contatos { get; set; }
    }
}
