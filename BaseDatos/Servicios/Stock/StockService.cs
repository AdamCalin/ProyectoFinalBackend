using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Stock.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos
{
	public interface IStockService
	{
		public Task<List<V_STOCK>> GetStock();
	}

	public class StockService : IStockService
	{
		public StockDbContext _context;
		public StockService(StockDbContext context)
		{
			this._context = context;
		}


		public Task<List<V_STOCK>> GetStock()
		{
			return  _context.V_STOCK.ToListAsync();
		}
	}
}
