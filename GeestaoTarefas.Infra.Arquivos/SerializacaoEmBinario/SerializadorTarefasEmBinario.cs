using GestaoTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.Infra.Arquivos
{
    public class SerializadorTarefasEmBinario : ISerializadorTarefas
    {
        private const string arquivoTarefas = @"C:\temp\tarefas.bin";
        
        public void GravarTarefasEmArquivo(List<Tarefa> tarefas)
        {
            BinaryFormatter serializador = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();

            serializador.Serialize(ms, tarefas);

            byte[] bytesTarefas = ms.ToArray();

            File.WriteAllBytes(arquivoTarefas, bytesTarefas);
        }

        public List<Tarefa> CarregarTarefasDoArquivo()
        {
            if (File.Exists(arquivoTarefas) == false)
                return new List<Tarefa>();

            BinaryFormatter serializador = new BinaryFormatter();

            byte[] bytesTarefas = File.ReadAllBytes(arquivoTarefas);

            MemoryStream ms = new MemoryStream(bytesTarefas);

            return (List<Tarefa>)serializador.Deserialize(ms);
        }
    }
}
