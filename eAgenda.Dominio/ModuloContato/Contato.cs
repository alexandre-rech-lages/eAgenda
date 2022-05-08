using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.ModuloContato
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
