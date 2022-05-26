using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public class ControladorCategoria : ControladorBase
    {
        private readonly IRepositorioCategoria repositorioCategoriaDespesa;
        private TabelaCategoriasControl tabelaCategoriasDespesa;


        public ControladorCategoria(IRepositorioCategoria repositorio)
        {
            repositorioCategoriaDespesa = repositorio;
        }

        public override void Inserir()
        {
            TelaCadastroCategoriasForm tela = new TelaCadastroCategoriasForm();

            tela.CategoriaDespesa = new Categoria();

            tela.GravarRegistro = repositorioCategoriaDespesa.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCategoriaDespesas();
            }
        }

        private void CarregarCategoriaDespesas()
        {
            List<Categoria> CategoriaDespesas = repositorioCategoriaDespesa.SelecionarTodos();

            tabelaCategoriasDespesa.AtualizarRegistros(CategoriaDespesas);
        }

        public override void Editar()
        {
            Categoria categoriaDespesaSelecionada = ObtemCategoriaDespesaSelecionada();

            if (categoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria de Despesa primeiro",
                    "Edição de Categoria de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroCategoriasForm tela = new TelaCadastroCategoriasForm();

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
            Categoria categoriaDespesaSelecionada = ObtemCategoriaDespesaSelecionada();

            if (categoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria de Despesa primeiro",
                    "Visualização de Categoria de Despesas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaVisualizacaoDespesasCategoriaForm tela = new TelaVisualizacaoDespesasCategoriaForm(categoriaDespesaSelecionada);

            tela.ShowDialog();
        }


        public override void Excluir()
        {
            Categoria CategoriaDespesaSelecionada = ObtemCategoriaDespesaSelecionada();

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
            return new ConfiguracaoToolboxCategoria();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaCategoriasDespesa == null)
                tabelaCategoriasDespesa = new TabelaCategoriasControl();

            CarregarCategoriaDespesas();

            return tabelaCategoriasDespesa;
        }

        private Categoria ObtemCategoriaDespesaSelecionada()
        {
            int numeroSelecionado = tabelaCategoriasDespesa.ObtemNumeroCategoriaDespesaSelecionada();

            Categoria CategoriaDespesaSelecionada = repositorioCategoriaDespesa.SelecionarPorNumero(numeroSelecionado);

            return CategoriaDespesaSelecionada;
        }
    }
}
