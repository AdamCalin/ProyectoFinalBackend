using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.EntityFrameworkCore;
using NEVER.BaseDatos.DTO.Usuarios;

namespace ConexionBaseDatos
{

	public interface IUsuarioService
	{
		Task<List<USUARIOS>> GetUsuario();

		Task<List<USUARIOS>> GetUsuarioId(int id_usuario);
		public ResponseCrearUsuario PostUsuario(CrearUsuarioDTO usuario);
	}

	public class UsuariosService : IUsuarioService
	{
		public UsuarioDbContext _context;
		public UsuariosService(UsuarioDbContext context)
		{
			this._context = context;
		}


		public async Task<List<USUARIOS>> GetUsuario()
		{
			return await _context.USUARIOS.ToListAsync();
		}

		public async  Task<List<USUARIOS>> GetUsuarioId(int id_usuario)
		{
			return _context.USUARIOS.Where(q => q.ID_USUARIO == id_usuario).ToList();
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
	}
}