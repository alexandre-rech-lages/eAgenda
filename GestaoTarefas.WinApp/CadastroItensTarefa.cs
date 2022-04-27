using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoTarefas.WinApp
{
    public partial class CadastroItensTarefa : Form
    {
        public CadastroItensTarefa(Tarefa tarefa)
        {
            InitializeComponent();

            labelTituloTarefa.Text = tarefa.Titulo;

            foreach (ItemTarefa item in tarefa.Itens)
            {
                listItensTarefa.Items.Add(item);
            }
        }

        public List<ItemTarefa> ItensAdicionados 
        {
            get 
            {
                return listItensTarefa.Items.Cast<ItemTarefa>().ToList();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ItemTarefa itemTarefa = new ItemTarefa();

            itemTarefa.Titulo = txtTituloItem.Text;

            listItensTarefa.Items.Add(itemTarefa);
        }
    }
}
