using System.Collections.ObjectModel;

namespace ASoftwareVersaoFisioterapiaAPI.Model
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataDoCadastro { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public ICollection<Cliente> Clientes { get; set; }

        public Usuario()
        {
            Clientes = new Collection<Cliente>();
        }
    }
}