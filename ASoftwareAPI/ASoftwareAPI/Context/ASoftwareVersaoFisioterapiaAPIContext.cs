using ASoftwareVersaoFisioterapiaAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ASoftwareVersaoFisioterapiaAPI.Context
{
    public class ASoftwareVersaoFisioterapiaAPIContext : DbContext
    {
        public ASoftwareVersaoFisioterapiaAPIContext(DbContextOptions<ASoftwareVersaoFisioterapiaAPIContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}