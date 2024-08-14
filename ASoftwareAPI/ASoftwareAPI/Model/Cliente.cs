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
        [RegularExpression(@"^\d+$", ErrorMessage = "Somente números inteiros são permitidos.")]
        public string Telefone { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O valor deve ser um número inteiro positivo.")]
        public int QuantidadeDeSessao { get; set; }

        [Required]
        public DateTime DataDaConsulta { get; set; }

        [Required]
        public DateTime DataDoCadastro { get; set; }

        [Required]
        public DateTime UltimaAtualizacao { get; set; }

        [Required]
        public Usuario Usuario { get; set; }

        [Required]
        public Categoria Categoria { get; set; }

        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }

        public Cliente()
        {
            Categoria = new Categoria();
        }
    }
}