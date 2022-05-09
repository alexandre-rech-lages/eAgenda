﻿using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public class ControladorDespesa : ControladorBase
    {
        private readonly IRepositorioDespesa repositorioDespesa;
        private ListagemDespesasControl listagemDespesas;


        public ControladorDespesa(IRepositorioDespesa repositorio)
        {
            repositorioDespesa = repositorio;
        }

        public override void Inserir()
        {
            TelaCadastroDespesasForm tela = new TelaCadastroDespesasForm();

            tela.Despesa = new Despesa();

            tela.GravarRegistro = repositorioDespesa.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDespesas();
            }
        }

        private void CarregarDespesas()
        {
            List<Despesa> despesas = repositorioDespesa.SelecionarTodos();

            listagemDespesas.AtualizarRegistros(despesas);
        }

        public override void Editar()
        {
            Despesa despesaSelecionada = listagemDespesas.SelecionarDespesa();

            if (despesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                    "Edição de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroDespesasForm tela = new TelaCadastroDespesasForm();

            tela.Despesa = despesaSelecionada;

            tela.GravarRegistro = repositorioDespesa.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDespesas();
            }
        }

        public override void Excluir()
        {
            Despesa despesaSelecionada = listagemDespesas.SelecionarDespesa();

            if (despesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                "Exclusão de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a despesa?",
                "Exclusão de Despesas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioDespesa.Excluir(despesaSelecionada);
                CarregarDespesas();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxDespesa();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemDespesas == null)
                listagemDespesas = new ListagemDespesasControl();

            CarregarDespesas();

            return listagemDespesas;
        }
    }
}
