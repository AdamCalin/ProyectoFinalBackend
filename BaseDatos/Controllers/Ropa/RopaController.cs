using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Ropa.Base_Datos;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{ 
		[ApiController]
		[Route("api/ropa")]
		public class RopaController : ControllerBase
		{

			public readonly IRopaService _service;
			private readonly RopaDbContext _context;

			public RopaController(IRopaService service, RopaDbContext context)
			{
				_service = service;
				_context = context;
			}
			[HttpGet("{id_articulo:int}")]
			public List<ROPA> Get(int id_articulo)
			{
				return _service.GetRopaId(id_articulo);
			}

			
		}
}
