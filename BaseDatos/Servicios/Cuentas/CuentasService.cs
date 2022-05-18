using ConexionBaseDatos;
using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;
using ConexionBaseDatos.BaseDatos.DTO.Cuentas;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{

	public interface ICuentasService{ 
	
		public LoginResponseDTO PostLogin(CredencialesRegister register);
		public LoginResponseDTO ComprobacionLogin(CredencialesLogin login);
	}

	public class CuentasService: ICuentasService
	{
		public CuentasDbContext _context;
		public CuentasService(CuentasDbContext context){
			_context = context;
		}
		public LoginResponseDTO PostLogin(CredencialesRegister register)
		{
			var mensaje = "";
			var retCode = 0;
			LoginResponseDTO response = new LoginResponseDTO();

			_context.PaComprobarLogin(register.email, register.pass, out mensaje, out retCode);

			response.mensaje = mensaje;
			response.retCode = retCode;

			return response;
		}
		public LoginResponseDTO ComprobacionLogin(CredencialesLogin login)
		{
	
			LoginResponseDTO response = new LoginResponseDTO();

			var retorno = _context.PaLogin( login.user, login.pass);

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;
			response.idUsuario = retorno.idUsuario;

			return response;

		}
	}
}
