using ConexionBaseDatos.BaseDatos;

namespace ConexionBaseDatos
{
	public interface IPedidosService
	{
		List<PEDIDOS> GetPedido();
	}

	public class PedidosService : IPedidosService
	{
		public DAWDbContext _context;
		public PedidosService(DAWDbContext context)
		{
			this._context = context;
		}


		public List<PEDIDOS> GetPedido()
		{
			return _context.PEDIDOS.ToList();
		}
	}
}
