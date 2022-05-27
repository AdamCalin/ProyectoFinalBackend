using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos.BaseDatos.Articulos.Base_Datos

{
	public class ArticuloDbContext: IdentityDbContext
	{
		public ArticuloDbContext(DbContextOptions<ArticuloDbContext> options) : base(options)
		{

		}
		//Set ARTICULOS 
		public DbSet<ARTICULOS> ARTICULOS { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//ENTITY ARTICULOS
			modelBuilder.Entity<ARTICULOS>(entity =>
			{
				entity.HasKey(o => o.ID_ARTICULO);
			});

		}
		//Obtencion de los datos front para pasarlos al PA de BBDD e insertar los datos en la tabla
		public void PaCrerarArticulo(string descripcion, string fabricante, int peso, int alto, int largo, int ancho, decimal precio, string n_registro, char talla, string color, byte[] imagen, out string mensaje, out int retCode)
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
					ParameterName = "DESCRIPCION",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 50,
					Value = descripcion,
				},

				new SqlParameter
				{
					ParameterName = "FABRICANTE",
					Value = fabricante,
					Size = 100,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},
				new SqlParameter
				{
					ParameterName = "PESO",
					Value = peso,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},
				new SqlParameter
				{
					ParameterName = "LARGO",
					Value = largo,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},
				new SqlParameter
				{
					ParameterName = "ANCHO",
					Value = ancho,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},
					new SqlParameter
				{
					ParameterName = "ALTO",
					Value = alto,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},
				new SqlParameter
				{
					ParameterName = "PRECIO",
					Value = precio,
					SqlDbType = System.Data.SqlDbType.Decimal,
				},
				new SqlParameter
				{
					ParameterName = "N_REGISTRO",
					Value = n_registro,
					SqlDbType = System.Data.SqlDbType.VarChar,
				},
				new SqlParameter
				{
					ParameterName = "TALLA",
					Value = n_registro,
					SqlDbType = System.Data.SqlDbType.Char,
					Size = 3
				},
				new SqlParameter
				{
					ParameterName = "COLOR",
					Value = n_registro,
					SqlDbType = System.Data.SqlDbType.VarChar,
					Size = 50
				},
				new SqlParameter
				{
					ParameterName = "IMAGEN",
					Value = n_registro,
					SqlDbType = System.Data.SqlDbType.Image
				},
				paramRETCODE,
				paramMENSAJE
			};

			this.Database.ExecuteSqlRaw("EXEC [dbo].[PA_CREAR_ARTICULO] @DESCRIPCION,	@FABRICANTE,	@PESO,	@LARGO,	@ANCHO,	@ALTO,	@PRECIO, @N_REGISTRO, @TALLA, @COLOR, @IMAGEN,	@RETCODE OUTPUT,	@MENSAJE OUTPUT", sqlParameters);

			retCode = (int)paramRETCODE.Value;
			mensaje = (string)paramMENSAJE.Value;
		}
	}

}
