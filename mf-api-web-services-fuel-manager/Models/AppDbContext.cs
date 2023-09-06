using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<VeiculoUsuarios>()
                .HasKey(vu => new { vu.VeiculoId, vu.UsuarioId });

            builder.Entity<VeiculoUsuarios>()
                .HasOne(vu => vu.Veiculo)
                .WithMany(v => v.Usuarios)
                .HasForeignKey(vu => vu.VeiculoId);
            
            builder.Entity<VeiculoUsuarios>()
                .HasOne(vu => vu.Usuario)
                .WithMany(u => u.Veiculos)
                .HasForeignKey(vu => vu.UsuarioId);
        }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VeiculoUsuarios> VeiculoUsuarios { get; set; }


    }
}
