using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Tienda.Base_Datos;

namespace ConexionBaseDatos
{
		public interface ITiendaService
		{
			public List<V_TIENDA> GetTienda();
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
		}

}
