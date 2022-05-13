using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos_Articulo.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{
	[ApiController]
	[Route("api/pedidos_articulos")]
	public class PedidosArticulosController : ControllerBase
	{

		public readonly IPedidos_articulosService _service;
		private readonly PedidosArticulosDbContext _context;

		public PedidosArticulosController(IPedidos_articulosService service, PedidosArticulosDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<PEDIDOS_ARTICULOS>> Get()
		{
			return _service.GetPedido_Articulo();
		}

		[HttpPost]
		public string Post(CrearPedido_ArticuloDTO pedido)
		{
			return _service.PostPedido_Articulo(pedido);
		}
	}
}