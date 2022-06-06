using ConexionBaseDatos.BaseDatos.DTO.Perfiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Perfiles.Base_Datos

{
	public class PerfilesDbContext : IdentityDbContext
	{
		public PerfilesDbContext(DbContextOptions<PerfilesDbContext> options) : base(options)
		{

		}
		//Set PERFILES 
		public DbSet<PerfilesDTO> PERFILES { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//ENTITY PERFILES
			modelBuilder.Entity<PerfilesDTO>(entity =>
			{
				entity.HasKey(o => o.id_perfil);
			});

		}
	}
}
