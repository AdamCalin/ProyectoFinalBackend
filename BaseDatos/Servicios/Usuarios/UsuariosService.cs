using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEVER.BaseDatos.DTO.Usuarios;

namespace ConexionBaseDatos
{

	public interface IUsuarioService
	{
		Task<List<ConsultaDatosUsuarioDTO>> GetUsuario();

		Task<ConsultaDatosUsuarioDTO> GetUsuarioId(int id_usuario);
		public ResponseCrearUsuario PostUsuario(CrearUsuarioDTO usuario);
		public Task<ResponseCrearUsuario> BorrarUsuario(int id_usuario);
		public Task<ResponseCrearUsuario> EditarUsuario(ConsultaDatosUsuarioDTO usuario);

	}

	public class UsuariosService : IUsuarioService
	{
		public UsuarioDbContext _context;
		public UsuariosService(UsuarioDbContext context)
		{
			this._context = context;
		}


		public async Task<List<ConsultaDatosUsuarioDTO>> GetUsuario()
		{
			return await _context.USUARIOS.ToListAsync();
		}

		public async Task<ConsultaDatosUsuarioDTO> GetUsuarioId(int id_usuario)
		{

			return await _context.USUARIOS.Where(q => q.ID_USUARIO == id_usuario).FirstOrDefaultAsync();
		}
		public ResponseCrearUsuario PostUsuario(CrearUsuarioDTO usuario)
		{
			var mensaje = "";
			var retCode = 0;
			ResponseCrearUsuario response = new ResponseCrearUsuario();

			var retorno = _context.PaCrearUsuario(usuario.usuario, usuario.pass, usuario.id_perfil, usuario.email);

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;

			return response;
		}
		public async Task<ResponseCrearUsuario> BorrarUsuario(int id_usuario)
		{
			var mensaje = "";
			var retCode = 0;

			var retorno =   _context.PaBorrarUsuario(id_usuario);
			ResponseCrearUsuario response = new ResponseCrearUsuario();

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;

			return response;


		}
		public async Task<ResponseCrearUsuario> EditarUsuario(ConsultaDatosUsuarioDTO usuario)
		{
			var mensaje = "";
			var retCode = 0;

			var retorno = _context.PaEditarUsuario(usuario.ID_USUARIO, usuario.USUARIO, usuario.EMAIL, usuario.ID_PERFIL);
			ResponseCrearUsuario response = new ResponseCrearUsuario();

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;

			return response;


		}

	}
}