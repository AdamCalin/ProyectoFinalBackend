using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{
	[ApiController]
	[Authorize(AuthenticationSchemes = "Bearer")]
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
			try
			{
				return _service.GetUsuario();
			}
			catch (Exception ex)
			{
				throw new Exception("UsuariosController.HttpGet.TryCatch", ex);
			}
		}

		[HttpPost]
		public string Post(CrearUsuarioDTO usuario)
		{
			try
			{
				return _service.PostUsuario(usuario);
			}
			catch (Exception ex)
			{
				throw new Exception("UsuariosController.HttpPost.TryCatch", ex);
			}
		}

		[HttpPut("{id_usuario:int}")]
		public ActionResult Put(USUARIOS usuario, int id_usuario)
		{
			try 
			{
				if (usuario.ID_USUARIO != id_usuario)
				{
					return BadRequest("El id del usuario no coincide");
				}

				_context.Update(usuario);
				_context.SaveChanges();
				return Ok();
			}
			catch(Exception ex)
			{
				throw new Exception("UsuariosController.HttpPut.TryCatch", ex);
			}
		}
	}
}