using GestaoTarefas.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.WinApp.ModuloCompromisso
{
    public class ConfiguracaoToolboxCompromisso : ConfiguracaoToolboxBase
    {
        public override string TooltipInserir => "Inserir um novo compromisso";

        public override string TooltipEditar => "Editar um compromisso existente";

        public override string TooltipExcluir => "Excluir um compromisso existente";
    }
}
