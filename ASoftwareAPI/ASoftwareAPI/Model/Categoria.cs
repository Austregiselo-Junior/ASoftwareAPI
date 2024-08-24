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
        [RegularExpression(@"^\d+$", ErrorMessage = "Somente números inteiros são permitidos.")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor deve ser um número inteiro positivo.")]
        public int QuantidadeDeSessao { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? ValorTotal { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? Desconto { get; set; }

        [Required]
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? ValorPago { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Somente números inteiros são permitidos.")]
        [Range(1, 31, ErrorMessage = "Adicione o dia do vencimento")]
        public string Vencimento { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Nome grande, tente um menor")]
        public string SituacaoFinanceira { get; set; }

        [Required]
        public DateTime UltimaAtualizacao { get; set; }

        public Cliente? Cliente { get; set; }
    }
}