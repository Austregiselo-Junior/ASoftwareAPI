namespace ASoftwareVersaoFisioterapiaAPI.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int QuantidadeDeSessao { get; set; }
        public DateTime DataDaConsulta { get; set; }
        public DateTime DataDoCadastro { get; set; }
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