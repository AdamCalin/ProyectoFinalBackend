using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Articulos.Base_Datos;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{

	public interface IArticuloService
	{
		public List<ARTICULOS> GetArticulo();
		public string PostArticulo(CrearArticuloDTO articulo);
	}

	public class ArticuloService: IArticuloService
	{
		public ArticuloDbContext _context;
		public ArticuloService(ArticuloDbContext context){
			_context = context;
		}


		public List<ARTICULOS> GetArticulo()
		{
			return _context.ARTICULOS.ToList();
		}

		public string PostArticulo(CrearArticuloDTO articulo)
		{
			try
			{
				var mensaje = "";
				var retCode = 0;
			
				_context.PaCrerarArticulo(articulo.descripcion, articulo.fabricante, articulo.peso, articulo.alto, articulo.largo, articulo.ancho, articulo.precio, articulo.n_registro, articulo.talla, articulo.color, articulo.imagen, out mensaje, out retCode);

				return "Articulo añadido correctamente";
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

	}
}
