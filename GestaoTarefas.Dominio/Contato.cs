namespace GestaoTarefas.Dominio
{
    public class Contato : EntidadeBase<Contato>
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public override void Atualizar(Contato registro)
        {
        }
    }
}
