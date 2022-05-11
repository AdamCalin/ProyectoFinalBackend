using ConexionBaseDatos.BaseDatos;
using Microsoft.AspNetCore.Mvc;


namespace ConexionBaseDatos.Controllers
{

	[ApiController]
	[Route("api/pedidos")]
	public class PedidosController : ControllerBase
	{

		public readonly IPedidosService _service;
		private readonly DAWDbContext _context;

		public PedidosController(IPedidosService service, DAWDbContext context)
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
		public ActionResult Post(PEDIDOS pedido)
		{
			_context.Add(pedido);
			_context.SaveChanges();
			return Ok();
		}
	}
}