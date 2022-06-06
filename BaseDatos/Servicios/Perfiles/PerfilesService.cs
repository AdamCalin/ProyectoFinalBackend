using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.DTO.Perfiles;
using ConexionBaseDatos.BaseDatos.Perfiles.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ConexionBaseDatos
{

	public interface IPerfilesService
	{
		public Task<List<PerfilesDTO>> GetPerfiles();
	}

	public class PerfilesService: IPerfilesService
	{
		public PerfilesDbContext _context;
		public PerfilesService(PerfilesDbContext context){
			_context = context;
		}


		public async Task<List<PerfilesDTO>> GetPerfiles()
		{
			return await _context.PERFILES.ToListAsync();
		}

	}
}
