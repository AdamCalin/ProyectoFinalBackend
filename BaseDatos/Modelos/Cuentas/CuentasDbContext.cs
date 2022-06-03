using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos

{
	public class CuentasDbContext : IdentityDbContext
	{
		public CuentasDbContext(DbContextOptions<CuentasDbContext> options) : base(options)
		{

		}
		//Set USUARIOS 
		public DbSet<USUARIOS> USUARIOS { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//ENTITY USUARIOS
			modelBuilder.Entity<USUARIOS>(entity =>
			{
				entity.HasKey(o => o.ID_USUARIO);
			});

		}
		//Obtencion de los datos front para pasarlos al PA de BBDD e insertar los datos en la tabla
		// PA_COMPROBAR_LOGIN
		public (string mensaje, int retcode, int id_usuario) PaRegister(string usuario, string pass, int? id_perfil, string email)
		{

			// PARAMETROS OUTPUT
			var paramIdUsuario = new SqlParameter
			{
				ParameterName = "IDUSUARIO",
				Direction = System.Data.ParameterDirection.Output,
				SqlDbType = System.Data.SqlDbType.Int,
			};
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
					Size = 100,
					Value = usuario,
				},
				new SqlParameter
				{
					ParameterName = "PASS",
					Value = pass,
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 300,
				},
				new SqlParameter
				{
					ParameterName = "ID_PERFIL",
					Value = DBNull.Value,
					SqlDbType = System.Data.SqlDbType.Int,
				},
				new SqlParameter
				{
					ParameterName = "EMAIL",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 100,
					Value = email,
				},
				
				paramIdUsuario,
				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_REGISTER] @USUARIO, @PASS, @ID_PERFIL, @EMAIL, @IDUSUARIO OUTPUT, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

			if ((int)paramRETCODE.Value < 0)
			{
				throw new Exception((string)paramMENSAJE.Value.ToString());
			}

			if ((int)paramRETCODE.Value > 0)
			{
				return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value, 0);
			}


			return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value, (int)paramIdUsuario.Value);
		}
		// PA_LOGIN
		public (string mensaje, int retcode, int idUsuario ) PaLogin(string user, string pass)
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

			var paramIDUUSARIO = new SqlParameter
			{
				ParameterName = "ID_USUARIO",
				Direction = System.Data.ParameterDirection.Output,
				SqlDbType = System.Data.SqlDbType.Int,
			};


			// PARAMETROS INPUT

			var sqlParameters = new[]
			{
				new SqlParameter
				{
					ParameterName = "USERNAME_EMAIL",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 100,
					Value = user,
				},

				new SqlParameter
				{
					ParameterName = "PASS",
					Value = pass,
					Size = 100,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},

				paramIDUUSARIO,
				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_LOGIN] @USERNAME_EMAIL, @PASS, @ID_USUARIO OUTPUT, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);


			if ((int)paramRETCODE.Value < 0)
			{
				throw new Exception((string)paramMENSAJE.Value.ToString());
			}

			if ((int)paramRETCODE.Value > 0)
			{
				return ((string)paramMENSAJE.Value, (int)paramRETCODE.Value, 0);
			}


			return (  (string)paramMENSAJE.Value,  (int)paramRETCODE.Value,  (int)paramIDUUSARIO.Value  );
		}
	}

}
