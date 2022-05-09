using eAgenda.Dominio.ModuloCompromisso;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloCompromisso
{
    public class RepositorioCompromissoEmArquivo : RepositorioEmArquivoBase<Compromisso>, IRepositorioCompromisso
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
            return new ValidadorCompromisso();
        }

        public List<Compromisso> SelecionarTodos(StatusCompromissoEnum status)
        {
            switch (status)
            {
                case StatusCompromissoEnum.Todos: return SelecionarTodos();

                case StatusCompromissoEnum.Futuros: return SelecionarCompromissosFuturos();

                case StatusCompromissoEnum.Passados: return SelecionarCompromissosPassados();

                default: return SelecionarTodos();
            }
        }

        private List<Compromisso> SelecionarCompromissosPassados()
        {
            return ObterRegistros().ToList();
        }

        private List<Compromisso> SelecionarCompromissosFuturos()
        {
            return ObterRegistros().ToList();
        }
    }
}
