using eAgenda.Dominio.ModuloCompromisso;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos.ModuloCompromisso
{
    public class RepositorioCompromissoEmArquivo : RepositorioEmArquivoBase<Compromisso>, IRepositorioCompromisso
    {
        public RepositorioCompromissoEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Compromissos.Count > 0)
                contador = dataContext.Compromissos.Max(x => x.Numero);
        }

        public override List<Compromisso> ObterRegistros()
        {
            return dataContext.Compromissos;
        }

        public override ValidationResult Inserir(Compromisso novoRegistro)
        {
            var validador = new ValidadorCompromisso();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            bool horarioOcupado = VerificarHorarioOcupado(novoRegistro.Data, novoRegistro.HoraInicio, novoRegistro.HoraTermino);

            if (horarioOcupado)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Nesta data e horário já tem um compromisso agendado"));

            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Numero = ++contador;

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }

            return resultadoValidacao;
        }

        public override AbstractValidator<Compromisso> ObterValidador()
        {
            return new ValidadorCompromisso();
        }

        public List<Compromisso> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
            return ObterRegistros()
                .Where(x => x.Data >= dataInicial)
                .Where(x => x.Data <= dataFinal)
                .ToList();
        }

        public List<Compromisso> SelecionarCompromissosPassados(DateTime hoje)
        {
            return ObterRegistros()
                .Where(x => x.Data < hoje)
                .ToList();
        }

        private bool VerificarHorarioOcupado(DateTime data, TimeSpan horaInicioDesejado, TimeSpan horaTerminoDesejado)
        {
            return ObterRegistros()
                .Where(x => x.Data == data)
                .Where(x => horaInicioDesejado >= x.HoraInicio && horaInicioDesejado <= x.HoraTermino)
                .Where(x => horaTerminoDesejado >= x.HoraInicio && horaTerminoDesejado <= x.HoraTermino)
                .Count() > 0;
        }
    }
}
