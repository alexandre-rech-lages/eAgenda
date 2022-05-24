using eAgenda.Dominio.Compartilhado;
using System;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public Despesa()
        {
            Data = DateTime.Now;
        }

        public Despesa(string descricao, decimal valor, DateTime data, FormaPgtoDespesaEnum formaPagamento, CategoriaDespesaEnum categoria) : this()
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            FormaPagamento = formaPagamento;
            Categoria = categoria;
        }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public CategoriaDespesaEnum Categoria { get; set; }

        public override void Atualizar(Despesa registro)
        {
            Descricao = registro.Descricao;
            Valor = registro.Valor;
            Data = registro.Data;
            FormaPagamento = registro.FormaPagamento;
            Categoria = registro.Categoria;
        }

        public override string ToString()
        {
            return $"Número: {Numero}, Descrição: {Descricao}, Data: {Data.ToShortDateString()}, Categoria: {Categoria}";
        }

        public Despesa Clonar()
        {
            return MemberwiseClone() as Despesa;
        }
    }
}
