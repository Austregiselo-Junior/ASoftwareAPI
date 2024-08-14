using System.ComponentModel.DataAnnotations;

namespace ASoftwareVersaoFisioterapiaAPI.Model
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Nome grande, tente um menor")]
        public string Nome { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? ValorDaSessao { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? ValorTotal { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? Desconto { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? Parcelas { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? ValorDasParcelas { get; set; }

        [Required]
        public DateTime Vencimento { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Nome grande, tente um menor")]
        public string SituacaoFinanceira { get; set; }

        [Required]
        public DateTime UltimaAtualizacao { get; set; }

        public Cliente Cliente { get; set; }
    }
}