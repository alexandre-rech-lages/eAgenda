using eAgenda.Dominio.ModuloDespesa;
using FluentValidation.Results;
using System;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloDespesa
{
    public partial class TelaCadastroCategoriasDespesaForm : Form
    {
        private CategoriaDespesa categoriaDespesa;

        public TelaCadastroCategoriasDespesaForm()
        {
            InitializeComponent();
        }

        public Func<CategoriaDespesa, ValidationResult> GravarRegistro { get; set; }


        public CategoriaDespesa CategoriaDespesa
        {
            get
            {
                return categoriaDespesa;
            }
            set
            {
                categoriaDespesa = value;

                txtNumero.Text = categoriaDespesa.Numero.ToString();
                txtTitulo.Text = categoriaDespesa.Titulo;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            categoriaDespesa.Titulo = txtTitulo.Text;

            ValidationResult resultadoValidacao = GravarRegistro(categoriaDespesa);

            if (resultadoValidacao.IsValid == false)
            {
                string primeiroErro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(primeiroErro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
