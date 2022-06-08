﻿using eAgenda.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Dominio.ModuloTarefa
{
    [Serializable]
    public class Tarefa : EntidadeBase<Tarefa>
    {
        private List<ItemTarefa> itens;

        public Tarefa()
        {
            Prioridade = PrioridadeTarefaEnum.Baixa;
            DataCriacao = DateTime.Now;
            itens = new List<ItemTarefa>();
        }

        public Tarefa(string t) : this()
        {
            Titulo = t;
            DataConclusao = null;
        }

        public string Titulo { get; set; }

        public PrioridadeTarefaEnum Prioridade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public List<ItemTarefa> Itens { get { return itens; } }
        public decimal PercentualConcluido { get; set; }

        public void CalcularPercentualConcluido()
        {
            if (itens.Count == 0)
            {
                PercentualConcluido = 0;
                return;
            }

            int qtdConcluidas = itens.Count(x => x.Concluido);

            var percentualConcluido = (qtdConcluidas / (decimal)itens.Count()) * 100;

            PercentualConcluido = Math.Round(percentualConcluido, 2);
        }

        public override string ToString()
        {
            var percentual = PercentualConcluido;

            if (DataConclusao.HasValue)
            {
                return $"Número: {Numero}, Título: {Titulo}, Percentual: {percentual}, Prioridade: {Prioridade}, " +
                    $"Concluída: {DataConclusao.Value.ToShortDateString()}";
            }

            return $"Número: {Numero}, Título: {Titulo}, Percentual: {percentual}, Prioridade: {Prioridade}";
        }

        public Tarefa Clonar()
        {
            return MemberwiseClone() as Tarefa;
        }

        public bool AdicionarItem(ItemTarefa item)
        {
            if (Itens.Exists(x => x.Equals(item)) == false)
            {
                item.Tarefa = this;
                itens.Add(item);
                DataConclusao = null;
                return true;
            }

            return false;
        }

        public void ConcluirItem(ItemTarefa item)
        {
            ItemTarefa itemTarefa = itens.Find(x => x.Equals(item));

            itemTarefa?.Concluir();

            if (itens.All(x => x.Concluido))
                DataConclusao = DateTime.Now.Date;

        }

        public void MarcarPendente(ItemTarefa item)
        {
            ItemTarefa itemTarefa = itens.Find(x => x.Equals(item));

            itemTarefa?.MarcarPendente();
        }

        public override void Atualizar(Tarefa registro)
        {
            Numero = registro.Numero;
            Titulo = registro.Titulo;
            Prioridade = registro.Prioridade;
        }

        public override bool Equals(object obj)
        {
            return obj is Tarefa tarefa &&
                   Numero == tarefa.Numero &&
                   Titulo == tarefa.Titulo &&
                   Prioridade == tarefa.Prioridade &&
                   DataCriacao.Date == tarefa.DataCriacao.Date &&
                   DataConclusao?.Date == tarefa.DataConclusao?.Date &&
                   PercentualConcluido == tarefa.PercentualConcluido;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Titulo, Prioridade, DataCriacao, DataConclusao, PercentualConcluido);
        }
    }
}
