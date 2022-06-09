using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEVER.BaseDatos.DTO.Usuarios;

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
		public Task<List<ConsultaDatosUsuarioDTO>> Get()
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
		public ResponseCrearUsuario Post(CrearUsuarioDTO usuario)
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
		
		[HttpPut]
		public async Task<ResponseCrearUsuario> Put(ConsultaDatosUsuarioDTO usuario)
		{
			try 
			{
				return await _service.EditarUsuario(usuario);
			}
			catch(Exception ex)
			{
				throw new Exception("UsuariosController.HttpPut.TryCatch", ex);
			}
		}

		[HttpGet("{id_usuario:int}")]
		public async Task<ConsultaDatosUsuarioDTO> Get(int id_usuario)
		{
			try
			{
				return await _service.GetUsuarioId(id_usuario);

			}
			catch (Exception ex)
			{
				throw new Exception("UsuariosController.HttpGet.TryCatch", ex);
			}
		}
		[HttpDelete("{id_usuario:int}")]
		public async Task<ResponseCrearUsuario> Delete(int id_usuario)
		{
			try
			{
				return await _service.BorrarUsuario(id_usuario);


			}
			catch (Exception ex)
			{
				throw new Exception("UsuariosController.HttpGet.TryCatch", ex);
			}
		}
	}
}