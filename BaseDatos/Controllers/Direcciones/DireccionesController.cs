using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{
	[ApiController]
	[Route("api/direcciones")]
	public class DireccionesController : ControllerBase
	{

		public readonly IDireccionesService _service;
		private readonly DireccionesDbContext _context;

		public DireccionesController(IDireccionesService service, DireccionesDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<DIRECCIONES>> Get()
		{
			return _service.GetDireccion();
		}

		[HttpPost]
		public string Post(CrearDireccionDTO direccion)
		{
			return _service.PostDireccion(direccion);
		}
		[HttpPut("{id_usuario:int}")]
		public ActionResult Put(DIRECCIONES direccion, int id_usuario)
		{
			if (direccion.ID_USUARIO != id_usuario)
			{
				return BadRequest("El id del usuario no coincide");
			}

			_context.Update(direccion);
			_context.SaveChanges();
			return Ok();
		}
	}
}