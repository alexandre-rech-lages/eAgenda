namespace GestaoTarefas.WinApp.Compartilhado
{
    public abstract class ConfiguracaoToolboxBase
    {
        public abstract string TooltipInserir { get; }

        public abstract string TooltipEditar { get; }

        public abstract string TooltipExcluir { get; }

        public virtual string TooltipAdicionarItens { get; }

        public virtual string TooltipAtualizarItens { get; }


        public virtual bool InserirHabilitado { get { return true; } }

        public virtual bool EditarHabilitado { get { return true; } }

        public virtual bool ExcluirHabilitado { get { return true; } }

        public virtual bool AdicionarItensHabilitado { get { return false; } }

        public virtual bool AtualizarItensHabilitado { get { return false; } }
    }
}