namespace ASoftwareVersaoFisioterapiaAPI.Model
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Tipo { get; set; }
        public int? ValorDaSessao { get; set; }
        public float? ValorTotal { get; set; }
        public float? Desconto { get; set; }
        public float? Parcelas { get; set; }
        public float? ValorDasParcelas { get; set; }
        public int? Vencimento { get; set; }
        public string SituacaoFinanceira { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
    }
}