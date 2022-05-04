using GeestaoTarefas.Infra.Arquivos;
using GeestaoTarefas.Infra.Arquivos.SerializacaoEmJson;
using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IRepositorioTarefa repositorioTarefa;
            IRepositorioContato repositorioContato;

            ISerializador serializador = new SerializadorDadosEmJsonDotnet();

            DataContext contextoDados = new DataContext();

            repositorioTarefa = new RepositorioTarefaEmArquivo(serializador, contextoDados);



            repositorioContato = new RepositorioContatoEmArquivo(serializador, contextoDados);

            Contato contato = new Contato() { Nome = "Rech", Numero = 65, Telefone = "321654" };

            Tarefa tarefa = new Tarefa();
            tarefa.ContatoSelecionado = contato;

            repositorioContato.Inserir(contato);

            repositorioTarefa.Inserir(tarefa);




            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TelaPrincipalForm());
        }
    }
}
