using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloContato;
using System;

namespace eAgenda.Dominio.ModuloCompromisso
{
    public class Compromisso : EntidadeBase<Compromisso>
    {
        public Compromisso()
        {
            Data = DateTime.Now;
            HoraInicio = DateTime.Now;
            HoraTermino = DateTime.Now.AddHours(1);
        }

        public Compromisso(string assunto, string local, string link, DateTime data,
             DateTime horaInicio, DateTime horaFim, Contato contato)
        {
            Assunto = assunto;
            Local = local;
            Link = link;
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaFim;
            Contato = contato;
        }

        public string Assunto { get; set; }
        
        public string Local { get; set; }

        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        public string Link { get; set; }
        public DateTime Data { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraTermino { get; set; }
        public Contato Contato { get; set; }



        public override void Atualizar(Compromisso registro)
        {
        }
    }
}
