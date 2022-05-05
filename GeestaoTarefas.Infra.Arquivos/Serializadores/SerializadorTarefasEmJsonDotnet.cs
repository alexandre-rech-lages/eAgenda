using GestaoTarefas.Dominio;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GestaoTarefas.Infra.Arquivos
{
    public class SerializadorTarefasEmJsonDotnet : ISerializadorTarefas
    {
        private const string arquivoTarefas = @"C:\temp\tarefas2.json";

        public List<Tarefa> CarregarTarefasDoArquivo()
        {
            if (File.Exists(arquivoTarefas) == false)
                return new List<Tarefa>();

            string tarefasJson = File.ReadAllText(arquivoTarefas);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            return JsonConvert.DeserializeObject<List<Tarefa>>(tarefasJson, settings);
        }

        public void GravarTarefasEmArquivo(List<Tarefa> tarefas)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            string tarefasJson = JsonConvert.SerializeObject(tarefas, settings);

            File.WriteAllText(arquivoTarefas, tarefasJson);
        }
    }
}
