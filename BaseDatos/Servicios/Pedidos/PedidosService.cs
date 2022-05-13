using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{
	public interface IPedidosService
	{
		List<PEDIDOS> GetPedido();
		public string PostPedido(CrearPedidoDTO pedido);
	}

	public class PedidosService : IPedidosService
	{
		public PedidosDbContext _context;
		public PedidosService(PedidosDbContext context)
		{
			this._context = context;
		}


		public List<PEDIDOS> GetPedido()
		{
			return _context.PEDIDOS.ToList();
		}
		public string PostPedido(CrearPedidoDTO pedido)
		{
			var mensaje = "";
			var retCode = 0;

			_context.PaCrearPedido( pedido.id_articulo, pedido.cantidad, pedido.precio, out mensaje, out retCode);

			return "Pedido realizado correctamente";
		}
	}
}
