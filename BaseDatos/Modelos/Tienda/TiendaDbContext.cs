using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Tienda.Base_Datos
{
	public class TiendaDbContext : DbContext
	{

		public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options)
		{

		}

		//Set STOCK
		public DbSet<V_TIENDA> V_TIENDA { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<V_TIENDA>(entity =>
			{
				entity.ToView("V_TIENDA");
				entity.Property(e => e.ID_ROPA).HasColumnName("ID_ROPA");
				entity.Property(e => e.ID_ARTICULO).HasColumnName("ID_ARTICULO");
				entity.Property(e => e.DESCRIPCION).HasColumnName("DESCRIPCION");
				entity.Property(e => e.PRECIO).HasColumnName("PRECIO");
				entity.Property(e => e.TALLA).HasColumnName("TALLA");
				entity.Property(e => e.COLOR).HasColumnName("COLOR");
				entity.Property(e => e.IMAGEN).HasColumnName("IMAGEN");
				entity.Property(e => e.SEXO).HasColumnName("SEXO");





			});



		}
	}
}
