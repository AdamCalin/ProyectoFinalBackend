using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{
	public interface IStockService
	{
		List<STOCK> GetStock();
		public string PostStock(CrearStockDTO stock);
	}

	public class StockService : IStockService
	{
		public StockDbContext _context;
		public StockService(StockDbContext context)
		{
			this._context = context;
		}


		public List<STOCK> GetStock()
		{
			return _context.STOCK.ToList();
		}
		public string PostStock(CrearStockDTO stock)
		{
			var mensaje = "";
			var retCode = 0;

			_context.PaCrearStock(stock.id_articulo, stock.cantidad_stock, stock.cantidad_pedido, stock.cantidad_envio, out mensaje, out retCode);

			//if (stock.id_articulo.Exists())
			//{
			//	return "EL articulo al cual quieres añadir stock ya existe";
			//}

			return "Stock del articulo " + "'" + stock.id_articulo + "'" + " añadido correctamente";
		}
	}
}
