using ConexionBaseDatos;
using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;
using ConexionBaseDatos.BaseDatos.DTO.Cuentas;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{

	public interface ICuentasService{ 
	
		public LoginResponseDTO PostLogin(CredencialesUsuario login);
	}

	public class CuentasService: ICuentasService
	{
		public CuentasDbContext _context;
		public CuentasService(CuentasDbContext context){
			_context = context;
		}
		public LoginResponseDTO PostLogin(CredencialesUsuario login)
		{
			var mensaje = "";
			var retCode = 0;
			LoginResponseDTO response = new LoginResponseDTO();

			_context.PaComprobarLogin(login.Email, login.Password, out mensaje, out retCode);

			response.mensaje = mensaje;
			response.retCode = retCode;

			return response;
		}
	}
}
