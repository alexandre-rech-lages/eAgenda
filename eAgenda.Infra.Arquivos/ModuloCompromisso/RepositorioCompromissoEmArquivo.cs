using eAgenda.Dominio.ModuloCompromisso;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloCompromisso
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
