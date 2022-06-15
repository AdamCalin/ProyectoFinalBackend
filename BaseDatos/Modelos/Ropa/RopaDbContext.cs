using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Ropa.Base_Datos
{
	public class RopaDbContext : IdentityDbContext
	{
		public RopaDbContext(DbContextOptions<RopaDbContext> options) : base(options)
		{

		}
		//Set ROPA 
		public DbSet<ROPA> ROPA { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//ENTITY ROPA
			modelBuilder.Entity<ROPA>(entity =>
			{
				entity.HasKey(o => o.ID_ROPA);
			});

		}
	}
}
