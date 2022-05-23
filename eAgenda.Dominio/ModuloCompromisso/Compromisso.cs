using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloContato;
using System;
using System.Collections.Generic;

namespace eAgenda.Dominio.ModuloCompromisso
{
    public class Compromisso : EntidadeBase<Compromisso>
    {
        private DateTime _date;
        private TipoLocalizacaoCompromissoEnum _compromissoEnum;
        public Compromisso()
        {
            Data = DateTime.Now;
            HoraInicio = Data.TimeOfDay;
            HoraTermino = Data.TimeOfDay;
        }

        public Compromisso(string assunto, string local, string link, DateTime data,
             TimeSpan horaInicio, TimeSpan horaFim, Contato contato)
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

        public TipoLocalizacaoCompromissoEnum TipoLocal
        {
            get { return _compromissoEnum; }
            set
            {
                _compromissoEnum = value;

                if (_compromissoEnum == TipoLocalizacaoCompromissoEnum.Presencial)
                {
                    Link = null;
                }
                else
                {
                    Local = null;
                }

            }
        }

        public string Link { get; set; }

        public DateTime Data { get { return _date.Date; } set { _date = value; } }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Contato Contato { get; set; }



        public override void Atualizar(Compromisso registro)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Compromisso compromisso &&
                   Numero == compromisso.Numero &&
                   Assunto == compromisso.Assunto &&
                   Local == compromisso.Local &&
                   TipoLocal == compromisso.TipoLocal &&
                   Link == compromisso.Link &&
                   Data == compromisso.Data &&
                   HoraInicio.Equals(compromisso.HoraInicio) &&
                   HoraTermino.Equals(compromisso.HoraTermino) &&
                   EqualityComparer<Contato>.Default.Equals(Contato, compromisso.Contato);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Numero);
            hash.Add(Assunto);
            hash.Add(Local);
            hash.Add(TipoLocal);
            hash.Add(Link);
            hash.Add(Data);
            hash.Add(HoraInicio);
            hash.Add(HoraTermino);
            hash.Add(Contato);
            return hash.ToHashCode();
        }
    }
}
