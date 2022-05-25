using FluentValidation;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class ValidadorCategoriaDespesa : AbstractValidator<CategoriaDespesa>
    {
        public ValidadorCategoriaDespesa()
        {
            RuleFor(x => x.Titulo)
                .NotNull()
                .NotEmpty();
        }
    }
}
