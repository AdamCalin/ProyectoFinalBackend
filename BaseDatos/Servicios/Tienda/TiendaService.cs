using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Tienda.Base_Datos;

namespace ConexionBaseDatos
{
		public interface ITiendaService
		{
			public List<V_TIENDA> GetTienda();
			public List<V_TIENDA> GetTiendaRopa(string color, int id_articulo);

		}

	public class TiendaService : ITiendaService
		{
			public TiendaDbContext _context;
			public TiendaService(TiendaDbContext context)
			{
				this._context = context;
			}


			public List<V_TIENDA> GetTienda()
			{
				return _context.V_TIENDA.ToList();
			}
			public List<V_TIENDA> GetTiendaRopa(string color, int id_articulo)
			{
				return _context.V_TIENDA.Where(q => q.ID_ARTICULO == id_articulo && q.COLOR == color).ToList();
			}
    }

}
