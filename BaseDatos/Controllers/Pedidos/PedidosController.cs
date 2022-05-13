using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{
	[ApiController]
	[Route("api/pedidos")]
	public class Pedidos_ArticulosController : ControllerBase
	{

		public readonly IPedidosService _service;
		private readonly PedidosDbContext _context;

		public Pedidos_ArticulosController(IPedidosService service, PedidosDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<PEDIDOS>> Get()
		{
			return _service.GetPedido();
		}

		[HttpPost]
		public string Post(CrearPedidoDTO pedido)
		{
			return _service.PostPedido(pedido);
		}
	}
}