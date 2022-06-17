using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Ropa.Base_Datos;
using Microsoft.EntityFrameworkCore;
using NEVER.BaseDatos.DTO.Ropa;

namespace ConexionBaseDatos
{

		public interface IRopaService
		{
			public List<ROPA> GetRopaId(int id_articulo);
			public Task<ResponseRopa> BorrarRopa(int id_ropa);
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

		public async Task<ResponseRopa> BorrarRopa(int id_ropa)
		{
			var mensaje = "";
			var retCode = 0;

			var retorno = _context.PaBorrarRopa(id_ropa);
			ResponseRopa response = new ResponseRopa();

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;

			return response;


		}

	}
}
