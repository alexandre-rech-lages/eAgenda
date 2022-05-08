using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloContato
{
    public class ValidadorContato : AbstractValidator<Contato>
    {
        public ValidadorContato()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty();
            RuleFor(x => x.Telefone).NotNull().NotEmpty();
        }
    }
}
