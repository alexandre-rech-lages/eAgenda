using eAgenda.Dominio.ModuloDespesa;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloDespesa
{
    public class RepositorioDespesaEmArquivo : RepositorioEmArquivoBase<Despesa>, IRepositorioDespesa
    {
        public RepositorioDespesaEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Despesas.Count > 0)
                contador = dataContext.Despesas.Max(x => x.Numero);
        }

        public ValidationResult Editar(Despesa novoRegistro, List<Categoria> categoriasMarcadas, List<Categoria> categoriasDesmarcadas)
        {
            throw new System.NotImplementedException();
        }

        public ValidationResult Inserir(Despesa novoRegistro, List<Categoria> categoriasMarcadas)
        {
            throw new System.NotImplementedException();
        }

        public override List<Despesa> ObterRegistros()
        {
            return dataContext.Despesas;
        }

        public override AbstractValidator<Despesa> ObterValidador()
        {
            return new ValidadorDespesa();
        }
    }
}
