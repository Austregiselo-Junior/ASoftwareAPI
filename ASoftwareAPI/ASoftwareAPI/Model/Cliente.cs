namespace ASoftwareVersaoFisioterapiaAPI.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Categoria { get; set; }
        public int QuantidadeDeSessao { get; set; }
        public DateTime DataDaConsulta { get; set; }
        public DateTime DataDoCadastro { get; set; }
        public DateTime DataDaAtualizacao { get; set; }
    }
}