using GestaoTarefas.WinApp.Compartilhado;

namespace GestaoTarefas.WinApp.ModuloCompromisso
{
    public class ConfiguracaoToolboxCompromisso : ConfiguracaoToolboxBase
    {
        public override string TooltipInserir => "Inserir um novo compromisso";

        public override string TooltipEditar => "Editar um compromisso existente";

        public override string TooltipExcluir => "Excluir um compromisso existente";
    }
}
