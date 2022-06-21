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
			public void PaCrearPedido(string usuario, string codigo, int estado, DateTime fecha, out string mensaje, out int retCode)
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
					ParameterName = "USUARIO",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = usuario,
				},
				new SqlParameter
				{
					ParameterName = "CODIGO",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = codigo,
				},
				new SqlParameter
				{
					ParameterName = "ESTADO",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = estado,
				},
				new SqlParameter
				{
					ParameterName = "FECHA",
					SqlDbType = System.Data.SqlDbType.Date,
					Value = fecha,
				},


				paramRETCODE,
				paramMENSAJE
			};

				this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_PEDIDO] @USUARIO, @CODIGO, @ESTADO, @FECHA, @RETCODE OUTPUT, @MENSAJE OUTPUT", sqlParameters);

				retCode = (int)paramRETCODE.Value;
				mensaje = (string)paramMENSAJE.Value;
			}

		public (string mensaje, int retCode) PaBorrarPedido(int id_pedido)
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
					ParameterName = "ID_PEDIDO",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = id_pedido,
				},


				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_BORRAR_PEDIDO] @ID_PEDIDO, @RETCODE OUTPUT, @MENSAJE OUTPUT", sqlParameters);


			if ((int)paramRETCODE.Value < 0)
			{
				throw new Exception((string)paramMENSAJE.Value.ToString());
			}

            //if ((int)paramRETCODE.Value > 0)
            //{
            //    return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value, 0);
            //}


            return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value);
		}

	}

}
