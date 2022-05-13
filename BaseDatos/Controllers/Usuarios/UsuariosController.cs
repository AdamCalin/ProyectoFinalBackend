using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{
	[ApiController]
	[Route("api/usuarios")]
	public class UsuariosController: ControllerBase
	{

		public readonly IUsuarioService _service;
		private readonly UsuarioDbContext _context;

		public UsuariosController(IUsuarioService service, UsuarioDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<USUARIOS>> Get()
		{
			return _service.GetUsuario();
		}

		[HttpPost]
		public string Post(CrearUsuarioDTO usuario)
		{
			return _service.PostUsuario(usuario);
		}

		[HttpPut("{id_usuario:int}")]
		public ActionResult Put(USUARIOS usuario, int id_usuario)
		{
			if (usuario.ID_USUARIO != id_usuario)
			{
				return BadRequest("El id del usuario no coincide");
			}

			_context.Update(usuario);
			_context.SaveChanges();
			return Ok();
		}
	}
}