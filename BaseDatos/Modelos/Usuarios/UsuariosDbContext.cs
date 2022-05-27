using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos
{
	public class UsuarioDbContext : IdentityDbContext
	{
		public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options)
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
		public (string mensaje, int retcode) PaCrearUsuario(string usuario, string pass, int id_perfil, string email)
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
					Size = 100,
					Value = usuario,
				},
				new SqlParameter
				{
					ParameterName = "PASS",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 100,
					Value = pass,
				},

				new SqlParameter
				{
					ParameterName = "ID_PERFIL",
					Value = id_perfil,
					SqlDbType = System.Data.SqlDbType.Int,
				},
				new SqlParameter
				{
					ParameterName = "EMAIL",
					Value = email,
					Size = 100,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},

				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_USUARIO] @USUARIO, @PASS, @ID_PERFIL, @EMAIL, @RETCODE OUTPUT, @MENSAJE OUTPUT", sqlParameters);


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
