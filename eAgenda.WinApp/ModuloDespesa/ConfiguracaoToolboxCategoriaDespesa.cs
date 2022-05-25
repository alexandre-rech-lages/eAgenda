using eAgenda.WinApp.Compartilhado;

namespace eAgenda.WinApp.ModuloDespesa
{
    public class ConfiguracaoToolboxCategoriaDespesa : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Categoria de Despesas";

        public override string TooltipInserir => "Inserir uma nova Categoria de Despesa";

        public override string TooltipEditar => "Editar uma Categoria de Despesa existente";

        public override string TooltipExcluir => "Excluir uma Categoria de Despesa existente";

        public override string TooltipVisualizar => "Visulizar as Despesas da Categoria";

        public override bool VisualizarHabilitado => true;
    }
}
