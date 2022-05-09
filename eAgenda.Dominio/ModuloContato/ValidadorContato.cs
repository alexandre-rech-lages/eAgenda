using FluentValidation;

namespace eAgenda.Dominio.ModuloContato
{
    public class ValidadorContato : AbstractValidator<Contato>
    {
        public ValidadorContato()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty();

            RuleFor(x => x.Email)
                .NotNull().NotEmpty();

            RuleFor(x => x.Telefone)
                .NotNull().NotEmpty();

            RuleFor(x => x.Empresa)
                .NotNull().NotEmpty();

            RuleFor(x => x.Cargo)
                .NotNull().NotEmpty();
        }
    }
}
