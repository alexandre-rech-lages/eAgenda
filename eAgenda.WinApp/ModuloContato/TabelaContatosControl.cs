﻿using eAgenda.Dominio.ModuloContato;
using eAgenda.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloContato
{
    public partial class TabelaContatosControl : UserControl
    {
        Subro.Controls.DataGridViewGrouper gridContatosAgrupados;
        private AgrupamentoContatoEnum tipoAgrupamento;

        public TabelaContatosControl()
        {
            InitializeComponent();

            grid.ConfigurarGridSomenteLeitura();
            grid.ConfigurarGridZebrado();
            grid.Columns.AddRange(ObterColunas());

            tipoAgrupamento = AgrupamentoContatoEnum.NaoAgrupar;
        }

        private DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Telefone", HeaderText = "Telefone"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Empresa", HeaderText = "Empresa"},

                new DataGridViewTextBoxColumn {DataPropertyName = "Cargo", HeaderText = "Cargo"}
            };

            return colunas;
        }

        public int ObtemNumeroContatoSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Contato> contatos)
        {
            DesagruparContatos();

            grid.DataSource = contatos;

            gridContatosAgrupados = new Subro.Controls.DataGridViewGrouper(grid);

            AgruparContatos();
        }

        public void DesagruparContatos()
        {
            if (gridContatosAgrupados == null)
                return;

            var campos = new string[] { "Cargo", "Empresa" };

            gridContatosAgrupados.RemoveGrouping();
            grid.RowHeadersVisible = true;

            foreach (var campo in campos)
                foreach (DataGridViewColumn item in grid.Columns)
                    if (item.DataPropertyName == campo)
                        item.Visible = true;
        }

        public void AgruparContatos(AgrupamentoContatoEnum tipoAgrupamento)
        {
            this.tipoAgrupamento = tipoAgrupamento;

            AgruparContatos();
        }

        private void AgruparContatos()
        {
            switch (tipoAgrupamento)
            {
                case AgrupamentoContatoEnum.AgruparPorEmpresa:
                    AgruparContatosPor("Empresa");
                    break;

                case AgrupamentoContatoEnum.AgruparPorCargo:
                    AgruparContatosPor("Cargo");
                    break;

                case AgrupamentoContatoEnum.NaoAgrupar:
                    DesagruparContatos();
                    break;

                default:
                    break;
            }
        }

        private void AgruparContatosPor(string campo)
        {
            if (gridContatosAgrupados == null)
                return;

            gridContatosAgrupados.RemoveGrouping();
            gridContatosAgrupados.SetGroupOn(campo);
            gridContatosAgrupados.Options.ShowGroupName = false;
            gridContatosAgrupados.Options.GroupSortOrder = SortOrder.None;

            foreach (DataGridViewColumn item in grid.Columns)
                if (item.DataPropertyName == campo)
                    item.Visible = false;

            grid.RowHeadersVisible = false;
            grid.ClearSelection();
        }


    }
}
