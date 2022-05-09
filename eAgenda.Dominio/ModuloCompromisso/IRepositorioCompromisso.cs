using eAgenda.Dominio.Compartilhado;
using System.Collections.Generic;

namespace eAgenda.Dominio.ModuloCompromisso
{
    public interface IRepositorioCompromisso : IRepositorio<Compromisso>
    {
        List<Compromisso> SelecionarTodos(StatusCompromissoEnum statusSelecioando);
    }
}
