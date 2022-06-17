using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Pedidos_Articulo.Base_Datos
{

		public class PedidosArticulosDbContext : IdentityDbContext
	{
			public PedidosArticulosDbContext(DbContextOptions<PedidosArticulosDbContext> options) : base(options)
			{

			}

			//Set PEDIDOS_ARTICULOS
			public DbSet<PEDIDOS_ARTICULOS> PEDIDOS_ARTICULOS { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
			base.OnModelCreating(modelBuilder);
			//ENTITY PEDIDOS_ARTICULOS
			modelBuilder.Entity<PEDIDOS_ARTICULOS>(entity =>
				{
					entity.HasKey(o => o.ID_PEDIDO_ARTICULO);
				});

			}
			//Obtencion de los datos front para pasarlos al PA de BBDD e insertar los datos en la tabla
			public void PaCrearPedido_Articulo(int id_articulo, string descripcion, char talla, string color,  int cantidad, decimal precio_und, decimal precio, out string mensaje, out int retCode)
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
					ParameterName = "DESCRIPCION",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = descripcion,
				},
				new SqlParameter
				{
					ParameterName = "TALLA",
					SqlDbType = System.Data.SqlDbType.Char,
					Value = talla,
				},
				new SqlParameter
				{
					ParameterName = "COLOR",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = color,
				},
				new SqlParameter
				{
					ParameterName = "CANTIDAD",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = cantidad,
				},
				new SqlParameter
				{
					ParameterName = "PRECIO_UNIDAD",
					Value = precio_und,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},
				new SqlParameter
				{
					ParameterName = "PRECIO",
					Value = precio,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},

				paramMENSAJE,
				paramRETCODE
			};

				this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_PEDIDO_ARTICULO] @ID_ARTICULO, @DESCRIPCION, @TALLA, @COLOR, @CANTIDAD, @PRECIO_UNIDAD, @PRECIO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

				retCode = (int)paramRETCODE.Value;
				mensaje = (string)paramMENSAJE.Value;
			}

		public (string mensaje, int retcode) PaBorrarPedido_Articulo(int id_pedido_articulo)
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
					ParameterName = "ID_PEDIDO_ARTICULO",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = id_pedido_articulo,
				},

				paramMENSAJE,
				paramRETCODE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_BORRAR_PEDIDO_ARTICULO] @ID_PEDIDO_ARTICULO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);


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
