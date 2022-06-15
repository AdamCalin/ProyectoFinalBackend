using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Ropa.Base_Datos;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos
{

		public interface IRopaService
		{
			public List<ROPA> GetRopaId(int id_articulo);
		}

	public class RopaService : IRopaService
	{
		public RopaDbContext _context;
		public RopaService(RopaDbContext context)
		{
			_context = context;
		}


		public List<ROPA> GetRopaId(int id_articulo)
		{

			return _context.ROPA.Where(q => q.ID_ARTICULO == id_articulo).ToList();
		}
	}
}
