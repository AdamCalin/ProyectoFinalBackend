using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Stock.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{

	[ApiController]
	[Authorize(AuthenticationSchemes = "Bearer")]
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
			try{
				return _service.GetStock();
			}
			catch (Exception ex)
			{
				throw new Exception("StockController.HttpGet.TryCatch", ex);
			}
		}

		[HttpPost]
		public string Post(CrearStockDTO stock)
		{
			try{
				return _service.PostStock(stock);
			}
			catch(Exception ex)
			{
				throw new Exception("StockController.HttpPost.TryCatch", ex);
			}
		}

		[HttpPut("{id_articulo:int}")]
		public ActionResult Put(STOCK articulo, int id_articulo)
		{
			try
			{
				if (articulo.ID_ARTICULO != id_articulo)
				{
					return BadRequest("El id del articulo no coincide");
				}

				_context.Update(articulo);
				_context.SaveChanges();
				return Ok();
			}
			catch (Exception ex)
			{
				throw new Exception("StockController.HttpPut.TryCatch", ex);
			}

		}
	}
}