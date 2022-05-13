using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Stock.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{

	[ApiController]
	[Route("api/stock")]
	public class StockController : ControllerBase
	{

		public readonly IStockService _service;
		private readonly StockDbContext _context;

		public StockController(IStockService service, StockDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<STOCK>> Get()
		{
			return _service.GetStock();
		}

		[HttpPost]
		public string Post(CrearStockDTO stock)
		{
			return _service.PostStock(stock);
		}

		[HttpPut("{id_articulo:int}")]
		public ActionResult Put(STOCK articulo, int id_articulo)
		{
			if (articulo.ID_ARTICULO != id_articulo)
			{
				return BadRequest("El id del articulo no coincide");
			}

			_context.Update(articulo);
			_context.SaveChanges();
			return Ok();
		}
	}
}