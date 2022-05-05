using GestaoTarefas.Dominio;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GestaoTarefas.Infra.Arquivos
{
    public class SerializadorTarefasEmXml : ISerializadorTarefas
    {
        private const string arquivoTarefas = @"C:\temp\tarefas.xml";

        public List<Tarefa> CarregarTarefasDoArquivo()
        {
            if (File.Exists(arquivoTarefas) == false)
                return new List<Tarefa>();

            XmlSerializer serializador = new XmlSerializer(typeof(List<Tarefa>));

            byte[] bytesTarefas = File.ReadAllBytes(arquivoTarefas);

            MemoryStream ms = new MemoryStream(bytesTarefas);

            return (List<Tarefa>)serializador.Deserialize(ms);
        }

        public void GravarTarefasEmArquivo(List<Tarefa> tarefas)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(List<Tarefa>));

            MemoryStream ms = new MemoryStream();

            serializador.Serialize(ms, tarefas);

            byte[] bytesTarefas = ms.ToArray();

            File.WriteAllBytes(arquivoTarefas, bytesTarefas);
        }


    }
}
