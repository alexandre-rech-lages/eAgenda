﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoTarefas.WinApp.ModuloContatos;
using GestaoTarefas.WinApp.ModuloTarefas;
using System;
using System.Windows.Forms;
using GestaoTarefas.Dominio;
using GestaoTarefas.Infra.Arquivos;
using GestaoTarefas.WinApp.Compartilhado;

namespace GestaoTarefas.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private IRepositorioTarefa repositorioTarefa;

        private ControladorBase controlador;

        public TelaPrincipalForm()
        {
            InitializeComponent();

            ISerializadorTarefas serializador = new SerializadorTarefasEmJsonDotnet();
            repositorioTarefa = new RepositorioTarefaEmArquivo(serializador);
        }

        private void tarefasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarToolbox(new ConfiguracaoToolboxTarefa());

            ListagemTarefasControl listagem = new ListagemTarefasControl();

            controlador = new ControladorTarefa(repositorioTarefa, listagem);

            CarregarListagemTarefas(listagem);
        }

        private void ConfigurarToolbox(ConfiguracaoToolboxBase configuracao)
        {
            ConfigurandoTooltips(configuracao);

            ConfigurandoBotoes(configuracao);
        }

        private void ConfigurandoBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnAdicionarItens.Enabled = configuracao.AdicionarItensHabilitado;
            btnAtualizarItens.Enabled = configuracao.AtualizarItensHabilitado;
        }

        private void ConfigurandoTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnAdicionarItens.ToolTipText = configuracao.TooltipAdicionarItens;
            btnAtualizarItens.ToolTipText = configuracao.TooltipAtualizarItens;
        }

        private void contatosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarToolbox(new ConfiguracaoToolboxContato());

            ListagemContatosControl listagem = new ListagemContatosControl();

            controlador = new ControladorContato();

            panelRegistros.Controls.Clear();

            listagem.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagem);
        }

        private void compromissosMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }


        private void CarregarListagemTarefas(ListagemTarefasControl listagem)
        {
            var tarefasPendentes = repositorioTarefa.SelecionarTarefasPendentes();

            var tarefasConcluidas = repositorioTarefa.SelecionarTarefasConcluidas();

            listagem.AtualizarRegistros(tarefasPendentes, tarefasConcluidas);

            listagem.Dock = DockStyle.Fill;

            panelRegistros.Controls.Clear();
            panelRegistros.Controls.Add(listagem);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnAdicionarItens_Click(object sender, EventArgs e)
        {
            controlador.AdicionarItens();
        }

        private void btnAtualizarItens_Click(object sender, EventArgs e)
        {
            controlador.AtualizarItens();
        }
    }
}
