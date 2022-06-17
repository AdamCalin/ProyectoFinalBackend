using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
		public (string mensaje, int retcode) PaBorrarRopa(int id_ropa)
		{

			// PARAMETROS OUTPUT
			var paramMENSAJE = new SqlParameter
			{
				ParameterName = "MENSAJE",
				Direction = System.Data.ParameterDirection.Output,
				SqlDbType = System.Data.SqlDbType.VarChar,
				Size = 100
			};
			var paramRETCODE = new SqlParameter
			{
				ParameterName = "RETCODE",
				Direction = System.Data.ParameterDirection.Output,
				SqlDbType = System.Data.SqlDbType.Int,
			};

			


			// PARAMETROS INPUT

			var sqlParameters = new[]
			{
				new SqlParameter
				{
					ParameterName = "ID_ROPA",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = id_ropa,
				},

				paramMENSAJE,
				paramRETCODE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_BORRAR_ROPA] @ID_ROPA, @MENSAJE OUTPUT, @RETCODE", sqlParameters);


			if ((int)paramRETCODE.Value < 0)
			{
				throw new Exception((string)paramMENSAJE.Value.ToString());
			}

			//if ((int)paramRETCODE.Value > 0)
			//{
			//	return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value, 0);
			//}


			return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value);
		}
	}
}
