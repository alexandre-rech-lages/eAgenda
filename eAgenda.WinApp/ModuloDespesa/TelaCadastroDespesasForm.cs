using eAgenda.Dominio.ModuloDespesa;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public partial class TelaCadastroDespesasForm : Form
    {

        public TelaCadastroDespesasForm(List<Categoria> categorias)
        {
            InitializeComponent();

            CarregarFormaPgto();

            CarregarCategorias(categorias);
        }

        private void CarregarCategorias(List<Categoria> categorias)
        {
            foreach (var item in categorias)
            {
                listCategorias.Items.Add(item);
            }
        }

        private void CarregarFormaPgto()
        {
            var formas = Enum.GetValues(typeof(FormaPgtoDespesaEnum));

            foreach (var item in formas)
            {
                cmbFormaPgto.Items.Add(item);
            }
        }

        private Despesa despesa;

        public Func<Despesa, ValidationResult> GravarRegistro { get; set; }

        public Despesa Despesa
        {
            get
            {
                return despesa;
            }
            set
            {
                despesa = value;

                txtNumero.Text = despesa.Numero.ToString();
                txtDescricao.Text = despesa.Descricao;
                txtValor.Text = despesa.Valor.ToString();
                txtData.Value = despesa.Data;
                cmbFormaPgto.SelectedItem = despesa.FormaPagamento;

                #region seleção de várias categorias
                int i = 0;

                for (int j = 0; j < listCategorias.Items.Count; j++)
                {
                    var categoria = (Categoria)listCategorias.Items[j];

                    if (despesa.Categorias.Contains(categoria))
                        listCategorias.SetItemChecked(i, true);

                    i++;
                }
                #endregion

                //cmbCategoria.SelectedItem = despesa.Categoria;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            despesa.Descricao = txtDescricao.Text;
            despesa.Valor = Convert.ToDecimal(txtValor.Text);
            despesa.Data = txtData.Value;
            despesa.FormaPagamento = (FormaPgtoDespesaEnum)cmbFormaPgto.SelectedItem;

            #region seleção de várias categorias

            var categorias = listCategorias.CheckedItems.Cast<Categoria>().ToList();

            despesa.AtribuirCategorias(categorias);

            #endregion

            //despesa.Categoria = (CategoriaEnum)cmbCategoria.SelectedItem;

            ValidationResult resultadoValidacao = GravarRegistro(despesa);

            if (resultadoValidacao.IsValid == false)
            {
                string primeiroErro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(primeiroErro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
