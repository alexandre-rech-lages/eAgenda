using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloCompromisso
{
    public partial class TelaCadastroCompromissosForm : Form
    {
        private Compromisso compromisso;
        private readonly ControladorCompromisso controladorCompromissos;

        public TelaCadastroCompromissosForm(List<Contato> contatos)
        {
            InitializeComponent();

            CarregarContatos(contatos);
        }


        private void CarregarContatos(List<Contato> contatos)
        {
            cmbContatos.Items.Clear();

            foreach (var item in contatos)
            {
                cmbContatos.Items.Add(item);
            }
        }

        public Func<Compromisso, ValidationResult> GravarRegistro { get; set; }

        public Compromisso Compromisso
        {
            get { return compromisso; }
            set
            {
                compromisso = value;

                txtNumero.Text = compromisso.Numero.ToString();

                txtAssunto.Text = compromisso.Assunto;

                txtLocal.Text = compromisso.Local;

                txtData.Value = compromisso.Data;

                txtHoraInicio.Value = compromisso.HoraInicio;

                txtHoraTermino.Value = compromisso.HoraTermino;

                cmbContatos.Enabled = compromisso.Contato != null;

                checkMarcarContato.Checked = compromisso.Contato != null;

                cmbContatos.SelectedItem = compromisso.Contato;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            compromisso.Assunto = txtAssunto.Text;
            compromisso.Local = txtLocal.Text;
            compromisso.Link = txtLink.Text;
            compromisso.Data = txtData.Value;
            compromisso.HoraInicio = txtHoraInicio.Value;
            compromisso.HoraTermino = txtHoraTermino.Value;
            compromisso.Contato = (Contato)cmbContatos.SelectedItem;

            var resultadoValidacao = GravarRegistro(compromisso);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void TelaCadastroCompromissosForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void TelaCadastroCompromisssosForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void checkMarcarContato_CheckedChanged(object sender, EventArgs e)
        {
            cmbContatos.Enabled = checkMarcarContato.Checked;
            cmbContatos.SelectedIndex = -1;
        }
    }
}
