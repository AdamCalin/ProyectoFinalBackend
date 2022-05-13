using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos
{

		public class PedidosDbContext : IdentityDbContext
	{
			public PedidosDbContext(DbContextOptions<PedidosDbContext> options) : base(options)
			{

			}

			//Set PEDIDOS
			public DbSet<PEDIDOS> PEDIDOS { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
			base.OnModelCreating(modelBuilder);
			//ENTITY PEDIDOS
			modelBuilder.Entity<PEDIDOS>(entity =>
				{
					entity.HasKey(o => o.ID_PEDIDO);
				});

			}
			//Obtencion de los datos front para pasarlos al PA de BBDD e insertar los datos en la tabla
			public void PaCrearPedido(int id_articulo, int cantidad, decimal precio, out string mensaje, out int retCode)
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
					ParameterName = "CANTIDAD",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = cantidad,
				},

				new SqlParameter
				{
					ParameterName = "PRECIO",
					Value = precio,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},

				paramRETCODE,
				paramMENSAJE
			};

				this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_PEDIDO] @ID_ARTICULO, @CANTIDAD, @PRECIO, @RETCODE OUTPUT, @MENSAJE OUTPUT", sqlParameters);

				retCode = (int)paramRETCODE.Value;
				mensaje = (string)paramMENSAJE.Value;
			}

		}

}
