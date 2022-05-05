using GestaoTarefas.Dominio;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GestaoTarefas.Infra.Arquivos
{
    internal class SerializadorTarefasEmJson : ISerializadorTarefas
    {
        private const string arquivoTarefas = @"C:\temp\tarefas.json";

        public List<Tarefa> CarregarTarefasDoArquivo()
        {
            if (File.Exists(arquivoTarefas) == false)
                return new List<Tarefa>();

            string tarefasJson = File.ReadAllText(arquivoTarefas);

            return JsonSerializer.Deserialize<List<Tarefa>>(tarefasJson);
        }

        public void GravarTarefasEmArquivo(List<Tarefa> tarefas)
        {
            var config = new JsonSerializerOptions { WriteIndented = true };

            string tarefasJson = JsonSerializer.Serialize(tarefas, config);

            File.WriteAllText(arquivoTarefas, tarefasJson);
        }
    }
}
