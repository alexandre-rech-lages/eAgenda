using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public class ControladorCategoriaDespesa : ControladorBase
    {
        private readonly IRepositorioCategoriaDespesa repositorioCategoriaDespesa;
        private TabelaCategoriasDespesaControl tabelaCategoriasDespesa;


        public ControladorCategoriaDespesa(IRepositorioCategoriaDespesa repositorio)
        {
            repositorioCategoriaDespesa = repositorio;
        }

        public override void Inserir()
        {
            TelaCadastroCategoriasDespesaForm tela = new TelaCadastroCategoriasDespesaForm();

            tela.CategoriaDespesa = new CategoriaDespesa();

            tela.GravarRegistro = repositorioCategoriaDespesa.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCategoriaDespesas();
            }
        }

        private void CarregarCategoriaDespesas()
        {
            List<CategoriaDespesa> CategoriaDespesas = repositorioCategoriaDespesa.SelecionarTodos();

            tabelaCategoriasDespesa.AtualizarRegistros(CategoriaDespesas);
        }

        public override void Editar()
        {
            CategoriaDespesa categoriaDespesaSelecionada = ObtemCategoriaDespesaSelecionada();

            if (categoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria de Despesa primeiro",
                    "Edição de Categoria de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroCategoriasDespesaForm tela = new TelaCadastroCategoriasDespesaForm();

            tela.CategoriaDespesa = categoriaDespesaSelecionada.Clonar();

            tela.GravarRegistro = repositorioCategoriaDespesa.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCategoriaDespesas();
            }
        }

        public override void Visualizar()
        {
            CategoriaDespesa categoriaDespesaSelecionada = ObtemCategoriaDespesaSelecionada();

            if (categoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria de Despesa primeiro",
                    "Visualização de Categoria de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaVisualizacaoCategoriaDespesaForm tela = new TelaVisualizacaoCategoriaDespesaForm(categoriaDespesaSelecionada);

            tela.ShowDialog();
        }


        public override void Excluir()
        {
            CategoriaDespesa CategoriaDespesaSelecionada = ObtemCategoriaDespesaSelecionada();

            if (CategoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria de Despesa primeiro",
                "Exclusão de Categorias de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Categoria de Despesa?",
                "Exclusão de Categorias de Despesas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioCategoriaDespesa.Excluir(CategoriaDespesaSelecionada);
                CarregarCategoriaDespesas();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxCategoriaDespesa();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaCategoriasDespesa == null)
                tabelaCategoriasDespesa = new TabelaCategoriasDespesaControl();

            CarregarCategoriaDespesas();

            return tabelaCategoriasDespesa;
        }

        private CategoriaDespesa ObtemCategoriaDespesaSelecionada()
        {
            int numeroSelecionado = tabelaCategoriasDespesa.ObtemNumeroCategoriaDespesaSelecionada();

            CategoriaDespesa CategoriaDespesaSelecionada = repositorioCategoriaDespesa.SelecionarPorNumero(numeroSelecionado);

            return CategoriaDespesaSelecionada;
        }
    }
}
