using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public class ControladorDespesa : ControladorBase
    {
        private readonly IRepositorioCategoria repositorioCategoria;
        private readonly IRepositorioDespesa repositorioDespesa;
        private TabelaDespesasControl tabelaDespesas;


        public ControladorDespesa(IRepositorioDespesa repositorio, IRepositorioCategoria repositorioCategoria)
        {
            repositorioDespesa = repositorio;
            this.repositorioCategoria = repositorioCategoria;
        }

        public override void Inserir()
        {
            var categorias = repositorioCategoria.SelecionarTodos();
            TelaCadastroDespesasForm tela = new TelaCadastroDespesasForm(categorias);

            tela.Despesa = new Despesa();

            tela.InserirRegistro = repositorioDespesa.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDespesas();
            }
        }

        private void CarregarDespesas()
        {
            List<Despesa> despesas = repositorioDespesa.SelecionarTodos();

            tabelaDespesas.AtualizarRegistros(despesas);
        }

        public override void Editar()
        {
            Despesa despesaSelecionada = ObtemDespesaSelecionada();

            if (despesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma despesa primeiro",
                    "Edição de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var categorias = repositorioCategoria.SelecionarTodos();
            TelaCadastroDespesasForm tela = new TelaCadastroDespesasForm(categorias);

            tela.Despesa = despesaSelecionada.Clonar();

            tela.EditarRegistro = repositorioDespesa.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarDespesas();
            }
        }



        public override void Excluir()
        {
            Despesa despesaSelecionada = ObtemDespesaSelecionada();

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
            if (tabelaDespesas == null)
                tabelaDespesas = new TabelaDespesasControl();

            CarregarDespesas();

            return tabelaDespesas;
        }

        private Despesa ObtemDespesaSelecionada()
        {
            int numeroSelecionado = tabelaDespesas.ObtemNumeroDespesaSelecionada();

            Despesa despesaSelecionada = repositorioDespesa.SelecionarPorNumero(numeroSelecionado);

            return despesaSelecionada;
        }
    }
}
