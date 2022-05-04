using GestaoTarefas.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefas.WinApp.ModuloTarefas
{
    public class ConfiguracaoToolboxTarefa : ConfiguracaoToolboxBase
    {
        public override string TooltipInserir { get { return "Inserir uma nova tarefa"; } }

        public override string TooltipEditar { get { return "Editar uma tarefa existente"; } }

        public override string TooltipExcluir { get { return "Excluir uma tarefa existente"; } }

        public override string TooltipAdicionarItens => "Adicionar itens para uma tarefa";

        public override string TooltipAtualizarItens => "Atualizar itens da tarefa";

        public override bool AdicionarItensHabilitado => true;

        public override bool AtualizarItensHabilitado => true;
    }
}
