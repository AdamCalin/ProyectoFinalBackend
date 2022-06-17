using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos_Articulo.Base_Datos;
using ConexionBaseDatos.DTOs;
using NEVER.BaseDatos.DTO.Pedidos_Articulos;

namespace ConexionBaseDatos
{
	public interface IPedidos_articulosService
	{
		List<PEDIDOS_ARTICULOS> GetPedido_Articulo();
		public ResponsePedidos_Articulos CrearPedido_Articulo(CrearPedido_ArticuloDTO pedido);

		public Task<ResponsePedidos_Articulos> BorrarPedido_Articulo(int id_pedido_articulo);
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
		public ResponsePedidos_Articulos CrearPedido_Articulo(CrearPedido_ArticuloDTO pedidoArticulo)
		{
			var mensaje = "";
			var retCode = 0;

			_context.PaCrearPedido_Articulo(pedidoArticulo.id_articulo, pedidoArticulo.descripcion, pedidoArticulo.talla, pedidoArticulo.color,  pedidoArticulo.cantidad, pedidoArticulo.precio_und, pedidoArticulo.precio, out mensaje, out retCode);

			ResponsePedidos_Articulos response = new ResponsePedidos_Articulos();

			response.mensaje = mensaje;
			response.retCode = retCode;

			return response;
		}
		public async Task<ResponsePedidos_Articulos> BorrarPedido_Articulo(int id_pedido_articulo)
		{
			var mensaje = "";
			var retCode = 0;

			var retorno = _context.PaBorrarPedido_Articulo(id_pedido_articulo);
			ResponsePedidos_Articulos response = new ResponsePedidos_Articulos();

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;

			return response;


		}

	}
}
