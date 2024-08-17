using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASoftwareVersaoFisioterapiaAPI.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nome grande, tente um menor")]
        public string Nome { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Somente números são permitidos.")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor deve ser um número inteiro positivo.")]
        public string Telefone { get; set; }

        [Required]
        public DateTime DataDaConsulta { get; set; }

        [Required]
        public DateTime DataDoCadastro { get; set; }

        [Required]
        public DateTime UltimaAtualizacao { get; set; }

        public Usuario Usuario { get; set; }

        public Categoria Categoria { get; set; }

        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }

        public Cliente()
        {
            Categoria = new Categoria();
        }
    }
}