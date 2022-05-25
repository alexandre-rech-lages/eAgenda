using eAgenda.Dominio.Compartilhado;
using System;
using System.Collections.Generic;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class CategoriaDespesa : EntidadeBase<CategoriaDespesa>
    {
        public CategoriaDespesa()
        {
            Despesas = new List<Despesa>();
        }

        public string Titulo { get; set; }

        public List<Despesa> Despesas { get; set; }

        public override void Atualizar(CategoriaDespesa registro)
        {
            Titulo = registro.Titulo;
        }

        public override string ToString()
        {
            return Titulo;
        }

        public override bool Equals(object obj)
        {
            return obj is CategoriaDespesa despesa &&
                   Numero == despesa.Numero &&
                   Titulo == despesa.Titulo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Titulo);
        }

        public CategoriaDespesa Clonar()
        {
            return MemberwiseClone() as CategoriaDespesa;
        }

        public void RegistrarDespesa(Despesa despesa)
        {
            if (Despesas.Contains(despesa) == false)
                Despesas.Add(despesa);
        }
    }
}