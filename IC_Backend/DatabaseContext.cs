using IC_Backend.Models;
using IC_Backend.ResponseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IC_Backend
{
    public partial class DatabaseContext : IdentityDbContext<Usuario>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Compra>()
                .HasOne(c => c.usuarioCompra)
                .WithMany(u => u.productosComprados)
                .HasForeignKey(c => c.usuarioCompraId);

            builder.Entity<Compra>()
                .HasOne(c => c.usuarioVenta)
                .WithMany(u => u.productosVendidos)
                .HasForeignKey(c => c.usuarioVentaId);

            //Generación uuid tablas modelo
            builder.Entity<Producto>(o =>
                o.Property(x => x.Id)
                .HasDefaultValue("uuid_generate_v4()")
                .ValueGeneratedOnAdd());
            builder.Entity<Compra>(o =>
                o.Property(x => x.Id)
                .HasDefaultValue("uuid_generate_v4()")
                .ValueGeneratedOnAdd());

            base.OnModelCreating(builder);
        }
    }
}
