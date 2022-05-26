using eAgenda.Dominio.Compartilhado;
using FluentValidation.Results;
using System.Collections.Generic;

namespace eAgenda.Dominio.ModuloDespesa
{
    public interface IRepositorioDespesa : IRepositorio<Despesa>
    {
        ValidationResult Inserir(Despesa novoRegistro, List<Categoria> categoriasMarcadas);

        ValidationResult Editar(Despesa novoRegistro, List<Categoria> categoriasMarcadas, List<Categoria> categoriasDesmarcadas);
    }
}
