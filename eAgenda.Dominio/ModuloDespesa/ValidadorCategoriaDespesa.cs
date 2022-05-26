using FluentValidation;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class ValidadorCategoriaDespesa : AbstractValidator<Categoria>
    {
        public ValidadorCategoriaDespesa()
        {
            RuleFor(x => x.Titulo)
                .NotNull()
                .NotEmpty();
        }
    }
}
