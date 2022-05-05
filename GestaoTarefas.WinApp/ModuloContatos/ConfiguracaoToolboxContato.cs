using GestaoTarefas.WinApp.Compartilhado;

namespace GestaoTarefas.WinApp.ModuloContatos
{
    public class ConfiguracaoToolboxContato : ConfiguracaoToolboxBase
    {
        public override string TooltipInserir { get { return "Inserir um novo contato"; } }

        public override string TooltipEditar { get { return "Editar um contato existente"; } }

        public override string TooltipExcluir { get { return "Excluir um contato existente"; } }

    }
}
