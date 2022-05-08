﻿using eAgenda.Dominio.ModuloTarefa;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloTarefa
{
    public partial class TelaCadastroTarefasForm : Form // View
    {
        private Tarefa tarefa;

        public TelaCadastroTarefasForm()
        {
            InitializeComponent();
        }

        public Func<Tarefa, ValidationResult> GravarRegistro { get; set; }

        public Tarefa Tarefa
        {
            get
            {
                return tarefa;
            }
            set
            {
                tarefa = value;
                txtNumero.Text = tarefa.Numero.ToString();
                txtTitulo.Text = tarefa.Titulo;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {            
            tarefa.Titulo = txtTitulo.Text;            

            var resultadoValidacao = GravarRegistro(tarefa);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
