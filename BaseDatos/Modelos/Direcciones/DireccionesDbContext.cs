using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos
{
	public class DireccionesDbContext : IdentityDbContext
	{
		public DireccionesDbContext(DbContextOptions<DireccionesDbContext> options) : base(options)
		{

		}

		//Set DIRECCIONES
		public DbSet<DIRECCIONES> DIRECCIONES { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//ENTITY DIRECCIONES
			modelBuilder.Entity<DIRECCIONES>(entity =>
			{
				entity.HasKey(o => o.ID_DIRECCION);
			});

		}
		//Obtencion de los datos front para pasarlos al PA de BBDD e insertar los datos en la tabla
		public void PaCrearDireccion(int id_usuario, string calle, string provincia, string poblacion, int cod, int numero, int piso, char puerta, string persona_contacto, string telefono, out string mensaje, out int retCode)
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
					ParameterName = "ID_USUARIO",
					SqlDbType = System.Data.SqlDbType.Int,
					Value = id_usuario,
				},
				new SqlParameter
				{
					ParameterName = "CALLE",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 100,
					Value = calle,
				},

				new SqlParameter
				{
					ParameterName = "PROVINCIA",
					Value = provincia,
					Size = 100,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},
				new SqlParameter
				{
					ParameterName = "POBLACION",
					Value = poblacion,
					Size = 100,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},
				new SqlParameter
				{
					ParameterName = "CODIGO_POSTAL",
					Value = cod,
					SqlDbType = System.Data.SqlDbType.Int,
				},
				new SqlParameter
				{
					ParameterName = "NUMERO",
					Value = numero,
					SqlDbType = System.Data.SqlDbType.Int,
				},
					new SqlParameter
				{
					ParameterName = "PISO",
					Value = piso,
					SqlDbType = System.Data.SqlDbType.Int,
				},
				new SqlParameter
				{
					ParameterName = "PUERTA",
					Value = puerta,
					SqlDbType = System.Data.SqlDbType.Char,
				},
				new SqlParameter
				{
					ParameterName = "PERSONA_CONTACTO",
					Value = persona_contacto,
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 100
				},
				new SqlParameter
				{
					ParameterName = "TELEFONO",
					Value = telefono,
					Size = 12,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},

				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_DIRECCION] @ID_USUARIO, @CALLE, @PROVINCIA, @POBLACION, @CODIGO_POSTAL, @NUMERO, @PISO, @PUERTA, @PERSONA_CONTACTO,@TELEFONO, @RETCODE OUTPUT,	@MENSAJE OUTPUT", sqlParameters);

			retCode = (int)paramRETCODE.Value;
			mensaje = (string)paramMENSAJE.Value;
		}

	}
}
