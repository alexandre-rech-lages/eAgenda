using GestaoTarefas.Dominio;
using System.Collections.Generic;

namespace GestaoTarefas.Infra.Arquivos
{
    public class RepositorioCompromissoEmArquivo : RepositorioEmArquivoBase<Compromisso>
    {
        public RepositorioCompromissoEmArquivo(DataContext dataContext) : base(dataContext)
        {

        }

        public override List<Compromisso> ObterRegistros()
        {
            return dataContext.Compromissos;
        }
    }
}
