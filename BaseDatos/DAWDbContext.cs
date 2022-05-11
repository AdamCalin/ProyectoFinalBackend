
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos
{

	public class DAWDbContext : DbContext
	{

		//Set PEDIDOS
		public DbSet<PEDIDOS> PEDIDOS { get; set; }

		//Set PEDIDOS_ARTICULOS
		public DbSet<PEDIDOS_ARTICULOS> PEDIDOS_ARTICULOS { get; set; }


		public DAWDbContext(DbContextOptions<DAWDbContext> options) : base(options)
		{

		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			//ENTITY PEDIDOS
			modelBuilder.Entity<PEDIDOS>(entity =>
			{
				entity.HasKey(o => o.ID_PEDIDO);
			});

			//ENTITY PEDIDOS_ARTICULOS
			modelBuilder.Entity<PEDIDOS_ARTICULOS>(entity =>
			{
				entity.HasKey(o => o.ID_ARTICULO);
			});




		}
		


	}

}
