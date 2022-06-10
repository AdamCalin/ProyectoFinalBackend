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
		public Task<List<V_STOCK>> Get()
		{
			try
			{
				return _service.GetStock();
			}
			catch (Exception ex)
			{
				throw new Exception("StockController.HttpGet.TryCatch", ex);
			}
		}
	}
}