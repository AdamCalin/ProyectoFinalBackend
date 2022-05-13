using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos_Articulo.Base_Datos;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{
	public interface IPedidos_articulosService
	{
		List<PEDIDOS_ARTICULOS> GetPedido_Articulo();
		public string PostPedido_Articulo(CrearPedido_ArticuloDTO pedido);
	}

	public class Pedidos_ArticulosService : IPedidos_articulosService
	{
		public PedidosArticulosDbContext _context;
		public Pedidos_ArticulosService(PedidosArticulosDbContext context)
		{
			this._context = context;
		}


		public List<PEDIDOS_ARTICULOS> GetPedido_Articulo()
		{
			return _context.PEDIDOS_ARTICULOS.ToList();
		}
		public string PostPedido_Articulo(CrearPedido_ArticuloDTO pedidoArticulo)
		{
			var mensaje = "";
			var retCode = 0;

			_context.PaCrearPedido_Articulo(pedidoArticulo.id_articulo, pedidoArticulo.cantidad, pedidoArticulo.precio, out mensaje, out retCode);

			return "Pedido realizado correctamente";
		}
	}
}
