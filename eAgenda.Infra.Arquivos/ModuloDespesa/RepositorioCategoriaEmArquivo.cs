using eAgenda.Dominio.ModuloDespesa;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloDespesa
{
    public class RepositorioCategoriaEmArquivo : RepositorioEmArquivoBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoriaEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Categorias.Count > 0)
                contador = dataContext.Categorias.Max(x => x.Numero);
        }

        public override List<Categoria> ObterRegistros()
        {
            return dataContext.Categorias;
        }

        public override AbstractValidator<Categoria> ObterValidador()
        {
            return new ValidadorCategoria();
        }
    }
}
