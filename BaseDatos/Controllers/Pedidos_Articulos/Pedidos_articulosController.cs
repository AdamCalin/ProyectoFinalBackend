using ConexionBaseDatos.BaseDatos;
using Microsoft.AspNetCore.Mvc;


namespace ConexionBaseDatos.Controllers
{

	[ApiController]
	[Route("api/pedidos_articulos")]
	public class Pedidos_articulosController : ControllerBase
	{

		public readonly IPedidos_articulosService _service;
		private readonly DAWDbContext _context;

		public Pedidos_articulosController(IPedidos_articulosService service, DAWDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<PEDIDOS_ARTICULOS>> Get()
		{
			return _service.GetPedido_articulo();
		}

		[HttpPost]
		public ActionResult Post(PEDIDOS_ARTICULOS pedido_articulo)
		{
			_context.Add(pedido_articulo);
			_context.SaveChanges();
			return Ok();
		}
	}
}