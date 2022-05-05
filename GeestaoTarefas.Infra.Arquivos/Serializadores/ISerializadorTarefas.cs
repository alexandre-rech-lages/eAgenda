using GestaoTarefas.Dominio;
using System.Collections.Generic;

namespace GestaoTarefas.Infra.Arquivos
{
    public interface ISerializadorTarefas
    {
        List<Tarefa> CarregarTarefasDoArquivo();
        void GravarTarefasEmArquivo(List<Tarefa> tarefas);
    }
}
