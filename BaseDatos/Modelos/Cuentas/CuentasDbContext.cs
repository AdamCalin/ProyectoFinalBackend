using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos

{
	public class CuentasDbContext: IdentityDbContext
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
		public void PaComprobarLogin(string email, string pass, out string mensaje, out int retCode)
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
					ParameterName = "EMAIl",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 100,
					Value = email,
				},

				new SqlParameter
				{
					ParameterName = "PASS",
					Value = pass,
					Size = 100,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},

				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_COMPROBAR_LOGIN] @EMAIL, @PASS,	@RETCODE OUTPUT, @MENSAJE OUTPUT", sqlParameters);

			retCode = (int)paramRETCODE.Value;
			mensaje = (string)paramMENSAJE.Value;
		}
	}

}
