using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Stock.Base_Datos
{
	public class StockDbContext : DbContext
	{
		public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
		{

		}

		//Set STOCK
		public DbSet<V_STOCK> V_STOCK { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<V_STOCK>(entity =>
			{
				entity.ToView("V_STOCK");
				entity.Property(e => e.ID_STOCK).HasColumnName("ID_STOCK");
				entity.Property(e => e.ID_ROPA).HasColumnName("ID_ROPA");
				entity.Property(e => e.ID_ARTICULO).HasColumnName("ID_ARTICULO");
				entity.Property(e => e.DESCRIPCION).HasColumnName("DESCRIPCION");
				entity.Property(e => e.COLOR).HasColumnName("COLOR");
				entity.Property(e => e.TALLA).HasColumnName("TALLA");
				entity.Property(e => e.CANTIDAD_STOCK).HasColumnName("CANTIDAD_STOCK");
				entity.Property(e => e.CANTIDAD_PEDIDO).HasColumnName("CANTIDAD_PEDIDO");
				entity.Property(e => e.CANTIDAD_ENVIO).HasColumnName("CANTIDAD_ENVIO");



			});



		}
	

	}
}
