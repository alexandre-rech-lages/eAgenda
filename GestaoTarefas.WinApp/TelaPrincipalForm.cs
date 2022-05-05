using System.Collections.Generic;
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
using GeestaoTarefas.Infra.Arquivos;
using GeestaoTarefas.Infra.Arquivos.SerializacaoEmJson;
using GestaoTarefas.WinApp.ModuloCompromisso;

namespace GestaoTarefas.WinApp
{
    public partial class TelaPrincipalForm : Form
    {        
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        
        public TelaPrincipalForm()
        {
            InitializeComponent();

            InicializarControladores();
        }
       

        private void tarefasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarToolbox(new ConfiguracaoToolboxTarefa());

            var opcaoSelecionada = (ToolStripMenuItem)sender;

            SelecionarControlador(opcaoSelecionada);

            CarregarListagem();
        }

        private void contatosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarToolbox(new ConfiguracaoToolboxContato());

            var opcaoSelecionada = (ToolStripMenuItem)sender;

            SelecionarControlador(opcaoSelecionada);

            CarregarListagem();
        }

        private void compromissosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarToolbox(new ConfiguracaoToolboxCompromisso());

            var opcaoSelecionada = (ToolStripMenuItem)sender;

            SelecionarControlador(opcaoSelecionada);

            CarregarListagem();
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

        private void CarregarListagem()
        {            
            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void SelecionarControlador(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];
        }

        private void InicializarControladores()
        {           
            controladores = new Dictionary<string, ControladorBase>();

            var serializador = new SerializadorDadosEmJsonDotnet();

            var contextoDados = new DataContext();

            var repositorioTarefa = new RepositorioTarefaEmArquivo(serializador, contextoDados);

            controladores.Add("Tarefas", new ControladorTarefa(repositorioTarefa));

            var repositorioContato = new RepositorioContatoEmArquivo(serializador, contextoDados);

            controladores.Add("Contatos", new ControladorContato(repositorioContato));
        }

       
    }
}
