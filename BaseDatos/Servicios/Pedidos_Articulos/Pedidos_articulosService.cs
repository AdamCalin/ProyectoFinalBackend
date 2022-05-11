using ConexionBaseDatos.BaseDatos;

namespace ConexionBaseDatos
{

	public interface IPedidos_articulosService
	{
		List<PEDIDOS_ARTICULOS> GetPedido_articulo();
	}

	public class Pedidos_articulosService : IPedidos_articulosService
	{
		public DAWDbContext _context;
		public Pedidos_articulosService(DAWDbContext context)
		{
			this._context = context;
		}


		public List<PEDIDOS_ARTICULOS> GetPedido_articulo()
		{
			return _context.PEDIDOS_ARTICULOS.ToList();
		}
	}
}
