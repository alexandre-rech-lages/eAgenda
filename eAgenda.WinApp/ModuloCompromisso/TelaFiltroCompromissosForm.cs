using eAgenda.Dominio.ModuloCompromisso;
using System.Windows.Forms;

namespace eAgenda.WinApp.ModuloCompromisso
{
    public partial class TelaFiltroCompromissosForm : Form
    {
        public TelaFiltroCompromissosForm()
        {
            InitializeComponent();
        }

        public StatusCompromissoEnum StatusSelecionado
        {
            get
            {
                if (rdbCompromissosPassados.Checked)
                    return StatusCompromissoEnum.Passados;

                else if (rdbCompromissosFuturos.Checked)
                    return StatusCompromissoEnum.Futuros;

                else
                    return StatusCompromissoEnum.Todos;
            }
        }
    }
}
