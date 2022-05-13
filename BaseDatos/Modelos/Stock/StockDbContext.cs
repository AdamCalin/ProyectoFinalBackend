using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Stock.Base_Datos
{
	public class StockDbContext : IdentityDbContext
	{
		public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
		{

		}

		//Set STOCK
		public DbSet<STOCK> STOCK { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//ENTITY STOCK
			modelBuilder.Entity<STOCK>(entity =>
			{
				entity.HasKey(o => o.ID_ARTICULO);
			});

		}
		//Obtencion de los datos front para pasarlos al PA de BBDD e insertar los datos en la tabla
		public void PaCrearStock(int id_articulo, int cantidad_stock, int cantidad_pedido, int cantidad_envio, out string mensaje, out int retCode)
		{

			// PARAMETROS OUTPUT

			var paramRETCODE = new SqlParameter
			{
				ParameterName = "RETCODE",
				Direction = System.Data.ParameterDirection.Output,
				SqlDbType = System.Data.SqlDbType.Int,
			};

			var paramMENSAJE = new SqlParameter
			{
				ParameterName = "MENSAJE",
				Direction = System.Data.ParameterDirection.Output,
				SqlDbType = System.Data.SqlDbType.VarChar,
				Size = 100
			};


			// PARAMETROS INPUT

			var sqlParameters = new[]
			{
				new SqlParameter
				{
					ParameterName = "ID_ARTICULO",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = id_articulo,
				},
				new SqlParameter
				{
					ParameterName = "CANTIDAD_STOCK",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = cantidad_stock,
				},
				new SqlParameter
				{
					ParameterName = "CANTIDAD_PEDIDO",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = cantidad_pedido,
				},
				new SqlParameter
				{
					ParameterName = "CANTIDAD_ENVIO",
					Value = cantidad_envio,
					SqlDbType = System.Data.SqlDbType.Int,
				},

				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_STOCK] @ID_ARTICULO, @CANTIDAD_STOCK, @CANTIDAD_PEDIDO, @CANTIDAD_ENVIO, @RETCODE OUTPUT, @MENSAJE OUTPUT", sqlParameters);

			retCode = (int)paramRETCODE.Value;
			mensaje = (string)paramMENSAJE.Value;
		}

	}
}
