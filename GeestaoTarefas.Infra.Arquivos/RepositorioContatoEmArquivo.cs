using GestaoTarefas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeestaoTarefas.Infra.Arquivos
{
    public class RepositorioContatoEmArquivo : IRepositorioContato
    {
        private readonly ISerializador serializador;
        private readonly DataContext dataContext;
        private int contador = 0;


        public RepositorioContatoEmArquivo(ISerializador serializador, DataContext dataContext)
        {
            this.serializador = serializador;
            this.dataContext = dataContext;

            dataContext.Contatos.AddRange( serializador.CarregarDadosDoArquivo().Contatos);

            //if (Contatos.Count > 0)
            //    contador = Contatos.Max(x => x.Numero);

        }

        public List<Contato> SelecionarTodos()
        {
            return dataContext.Contatos;
        }

        public void Inserir(Contato novaContato)
        {
            novaContato.Numero = ++contador;
            dataContext.Contatos.Add(novaContato);

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public void Editar(Contato Contato)
        {
            foreach (var item in dataContext.Contatos)
            {
                if (item.Numero == Contato.Numero)
                {
                    //item.Titulo = Contato.Titulo;
                    break;
                }
            }

            serializador.GravarDadosEmArquivo(dataContext);
        }

        public void Excluir(Contato Contato)
        {
            dataContext.Contatos.Remove(Contato);

            serializador.GravarDadosEmArquivo(dataContext);
        }

     

    }
}
