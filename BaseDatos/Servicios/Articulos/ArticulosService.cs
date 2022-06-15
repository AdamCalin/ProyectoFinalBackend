using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Articulos.Base_Datos;
using ConexionBaseDatos.DTOs;
using NEVER.BaseDatos.DTO.Articulos;

namespace ConexionBaseDatos
{

	public interface IArticuloService
	{
		public List<ARTICULOS> GetArticulo();
		public ResponseCrearArticulo PostArticulo(CrearArticuloDTO articulo);
	}

	public class ArticuloService: IArticuloService
	{
		public ArticulosDbContext _context;
		public ArticuloService(ArticulosDbContext context){
			_context = context;
		}


		public List<ARTICULOS> GetArticulo()
		{
			return _context.ARTICULOS.ToList(); 
		}

		public ResponseCrearArticulo PostArticulo(CrearArticuloDTO articulo)
		
		{

				var mensaje = "";
				var retCode = 0;
				var id_articulo = 0;

			var cantidad_envio = 0;
			var cantidad_pedido = 0;
			var cantidad_stock = 0;

			_context.PaCrerarArticulo(articulo.descripcion, articulo.fabricante, articulo.peso, articulo.largo, articulo.ancho, articulo.alto, articulo.precio, articulo.talla, articulo.color, articulo.n_registro, articulo.imagen, articulo.sexo, out mensaje, out retCode, out id_articulo, out cantidad_envio, out cantidad_pedido, out cantidad_stock);

			ResponseCrearArticulo response = new ResponseCrearArticulo();

			response.mensaje = mensaje;
			response.retCode = retCode;
			response.id_articulo = id_articulo;

			return response;
		}

	}
}
