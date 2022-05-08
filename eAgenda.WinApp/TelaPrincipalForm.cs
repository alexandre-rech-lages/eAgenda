using eAgenda.Infra.Arquivos;
using eAgenda.Infra.Arquivos.ModuloContato;
using eAgenda.Infra.Arquivos.ModuloTarefa;
using eAgenda.WinApp.Compartilhado;
using eAgenda.WinApp.ModuloCompromisso;
using eAgenda.WinApp.ModuloContato;
using eAgenda.WinApp.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;

        public TelaPrincipalForm(DataContext contextoDados)
        {
            InitializeComponent();

            this.contextoDados = contextoDados;

            InicializarControladores();
        }

        private void tarefasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void contatosMenuItem_Click(object sender, EventArgs e)
        {            
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void compromissosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
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

        private void ConfigurarToolbox(string tipo)
        {
            ConfiguracaoToolboxBase configuracao = null;

            if (tipo == "Tarefas")
                configuracao = new ConfiguracaoToolboxTarefa();

            else if (tipo == "Contatos")
                configuracao = new ConfiguracaoToolboxContato();

            else if (tipo == "Compromissos")
                configuracao = new ConfiguracaoToolboxCompromisso();

            if (configuracao != null)
            {
                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnAdicionarItens.Enabled = configuracao.AdicionarItensHabilitado;
            btnAtualizarItens.Enabled = configuracao.AtualizarItensHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnAdicionarItens.ToolTipText = configuracao.TooltipAdicionarItens;
            btnAtualizarItens.ToolTipText = configuracao.TooltipAtualizarItens;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            SelecionarControlador(tipo);

            ConfigurarToolbox(tipo);           

            ConfigurarListagem();
        }

        private void SelecionarControlador(string tipo)
        {
            controlador = controladores[tipo];
        }

        private void ConfigurarListagem()
        {            
            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void InicializarControladores()
        {
            var repositorioTarefa = new RepositorioTarefaEmArquivo(contextoDados);
            var repositorioContato = new RepositorioContatoEmArquivo(contextoDados);

            controladores = new Dictionary<string, ControladorBase>();
            controladores.Add("Tarefas", new ControladorTarefa(repositorioTarefa));
            controladores.Add("Contatos", new ControladorContato(repositorioContato));
        }
    }
}
