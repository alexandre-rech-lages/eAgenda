using eAgenda.Dominio;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos
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
