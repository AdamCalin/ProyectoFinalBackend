using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{

	public interface IUsuarioService
	{
		List<USUARIOS> GetUsuario();
		public string PostUsuario(CrearUsuarioDTO usuario);
	}

	public class UsuariosService : IUsuarioService
	{
		public UsuarioDbContext _context;
		public UsuariosService(UsuarioDbContext context)
		{
			this._context = context;
		}


		public List<USUARIOS> GetUsuario()
		{
			return _context.USUARIOS.ToList();
		}
		public string PostUsuario(CrearUsuarioDTO usuario)
		{
			var mensaje = "";
			var retCode = 0;

			_context.PaCrearUsuario(usuario.usuario, usuario.pass, usuario.id_perfil, usuario.email, out mensaje, out retCode);

			return "Usuario añadido con éxito";
		}
	}
}