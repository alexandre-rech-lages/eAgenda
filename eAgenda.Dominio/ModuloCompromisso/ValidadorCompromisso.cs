using FluentValidation;
using System;

namespace eAgenda.Dominio.ModuloCompromisso
{
    public class ValidadorCompromisso : AbstractValidator<Compromisso>
    {
        public ValidadorCompromisso()
        {
            RuleFor(x => x.Assunto)
               .NotNull().NotEmpty();

            RuleFor(x => x.Local)
               .NotNull().NotEmpty();

            RuleFor(x => x.Data)
               .NotNull().NotEmpty().GreaterThan((x) => DateTime.Now.Date);

            RuleFor(x => x.HoraInicio).LessThan(x => x.HoraTermino)
                .WithMessage("Horário de ínicio deve ser menor que Horário de Términio");
        }
    }
}
