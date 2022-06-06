using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Articulos.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{

	
	[ApiController]
	[Authorize(AuthenticationSchemes = "Bearer")]
	[Route("api/articulos")]
	public class ArticulosController : ControllerBase
	{

		public readonly IArticuloService _service;
		private readonly ArticulosDbContext _context;

		public ArticulosController(IArticuloService service, ArticulosDbContext context)
		{
			_service = service;
			_context = context;
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult<List<ARTICULOS>> Get()
		{
			try
			{
				return _service.GetArticulo();
			}
			catch (Exception ex)
			{
				throw new Exception("ArticulosController.HttpGet.", ex);
			}
		}
		
		[HttpPost]
		[AllowAnonymous]
		public string Post(CrearArticuloDTO articulo )
		{
			try {
				return _service.PostArticulo(articulo);
			}
			catch (Exception ex){
				throw new Exception("ArticulosController.HttpPost.", ex);
			}
			
			 
		}
		[HttpPut("{id_articulo:int}")]
		public ActionResult Put(ARTICULOS articulo, int id_articulo)
		{
			if (articulo.ID_ARTICULO != id_articulo)
			{
				return BadRequest("El id del articulo no coincide");
			}

			_context.Update(articulo);
			_context.SaveChanges();
			return Ok();
		}
	}
}