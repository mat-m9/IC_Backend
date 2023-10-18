using IC_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Generación uuid tablas modelo
            builder.Entity<Producto>(o =>
                o.Property(x => x.Id)
                .HasDefaultValue("uuid_generate_v4()")
                .ValueGeneratedOnAdd());

            base.OnModelCreating(builder);
        }
    }
}
