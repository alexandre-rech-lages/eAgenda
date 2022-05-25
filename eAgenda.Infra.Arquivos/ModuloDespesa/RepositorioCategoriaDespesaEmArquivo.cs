using eAgenda.Dominio.ModuloDespesa;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloDespesa
{
    public class RepositorioCategoriaDespesaEmArquivo : RepositorioEmArquivoBase<CategoriaDespesa>, IRepositorioCategoriaDespesa
    {
        public RepositorioCategoriaDespesaEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.CategoriaDespesas.Count > 0)
                contador = dataContext.CategoriaDespesas.Max(x => x.Numero);
        }

        public override List<CategoriaDespesa> ObterRegistros()
        {
            return dataContext.CategoriaDespesas;
        }

        public override AbstractValidator<CategoriaDespesa> ObterValidador()
        {
            return new ValidadorCategoriaDespesa();
        }
    }
}
