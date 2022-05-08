using eAgenda.Dominio.ModuloCompromisso;
using FluentValidation;
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

        public override AbstractValidator<Compromisso> ObterValidador()
        {
            throw new System.NotImplementedException();
        }
    }
}
