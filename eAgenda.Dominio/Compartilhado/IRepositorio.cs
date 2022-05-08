using FluentValidation.Results;
using System.Collections.Generic;

namespace eAgenda.Dominio.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        ValidationResult Inserir(T novaTarefa);

        ValidationResult Editar(T tarefa);

        ValidationResult Excluir(T tarefa);
        
        List<T> SelecionarTodos();

    }
}
