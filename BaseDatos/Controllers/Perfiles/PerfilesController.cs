using ConexionBaseDatos.BaseDatos.DTO.Perfiles;
using ConexionBaseDatos.BaseDatos.Perfiles.Base_Datos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.BaseDatos.Controllers.Perfiles
{

		[ApiController]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Route("api/perfiles")]
		public class PerfilesController : ControllerBase
		{

			public readonly IPerfilesService _service;
			private readonly PerfilesDbContext _context;

			public PerfilesController(IPerfilesService service, PerfilesDbContext context)
			{
				this._service = service;
				this._context = context;
			}
			[HttpGet]
			public async Task<List<PerfilesDTO>> Get()
			{
				return await _service.GetPerfiles();
			}
		}
	
}
