using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public class ControladorCategoria : ControladorBase
    {
        private readonly IRepositorioCategoria repositorioCategoria;
        private TabelaCategoriasControl tabelaCategorias;


        public ControladorCategoria(IRepositorioCategoria repositorio)
        {
            repositorioCategoria = repositorio;
        }

        public override void Inserir()
        {
            TelaCadastroCategoriasForm tela = new TelaCadastroCategoriasForm();

            tela.Categoria = new Categoria();

            tela.GravarRegistro = repositorioCategoria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCategorias();
            }
        }

        private void CarregarCategorias()
        {
            List<Categoria> Categorias = repositorioCategoria.SelecionarTodos();

            tabelaCategorias.AtualizarRegistros(Categorias);
        }

        public override void Editar()
        {
            Categoria categoriaDespesaSelecionada = ObtemCategoriaSelecionada();

            if (categoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria primeiro",
                    "Edição de Categoria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroCategoriasForm tela = new TelaCadastroCategoriasForm();

            tela.Categoria = categoriaDespesaSelecionada.Clonar();

            tela.GravarRegistro = repositorioCategoria.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCategorias();
            }
        }

        public override void Visualizar()
        {
            Categoria categoriaDespesaSelecionada = ObtemCategoriaSelecionada();

            if (categoriaDespesaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria primeiro",
                    "Visualização de Categoria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaVisualizacaoDespesasCategoriaForm tela = new TelaVisualizacaoDespesasCategoriaForm(categoriaDespesaSelecionada);

            tela.ShowDialog();
        }


        public override void Excluir()
        {
            Categoria CategoriaSelecionada = ObtemCategoriaSelecionada();

            if (CategoriaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Categoria primeiro",
                "Exclusão de Categorias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Categoria?",
                "Exclusão de Categorias", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioCategoria.Excluir(CategoriaSelecionada);
                CarregarCategorias();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxCategoria();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaCategorias == null)
                tabelaCategorias = new TabelaCategoriasControl();

            CarregarCategorias();

            return tabelaCategorias;
        }

        private Categoria ObtemCategoriaSelecionada()
        {
            int numeroSelecionado = tabelaCategorias.ObtemNumeroCategoriaSelecionada();

            Categoria CategoriaSelecionada = repositorioCategoria.SelecionarPorNumero(numeroSelecionado);

            return CategoriaSelecionada;
        }
    }
}
