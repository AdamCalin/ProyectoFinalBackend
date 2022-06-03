using ConexionBaseDatos;
using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;
using ConexionBaseDatos.BaseDatos.DTO.Cuentas;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NEVER.BaseDatos.DTO.Cuentas;
using System.Security.Cryptography;

namespace ConexionBaseDatos
{

	public interface ICuentasService{ 
	
		public LoginResponseDTO PostRegister(CredencialesRegister register);
		public LoginResponseDTO PostLogin(CredencialesLogin login);
	}

	public class CuentasService: ICuentasService
	{
		public CuentasDbContext _context;
		public CuentasService(CuentasDbContext context){
			_context = context;
		}
		public LoginResponseDTO PostRegister(CredencialesRegister register)
		{

			LoginResponseDTO response = new LoginResponseDTO();
			//ResultadoHash resHash = new ResultadoHash();
			//resHash = Hash(register.pass);
			var retorno = _context.PaRegister(register.user, register.pass, register.id_perfil, register.email);

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;
			response.idUsuario = retorno.id_usuario;


			return response;
		}
		public LoginResponseDTO PostLogin(CredencialesLogin login)
		{
	
			LoginResponseDTO response = new LoginResponseDTO();

			var retorno = _context.PaLogin( login.user, login.pass);

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retcode;
			response.idUsuario = retorno.idUsuario;

			return response;

		}

		public ResultadoHash Hash(string textoPlano)
		{
			var random = new RNGCryptoServiceProvider();

			// Maximum length of salt
			int max_length = 32;

			// Empty salt array
			byte[] salt = new byte[max_length];

			// Build the random bytes
			random.GetNonZeroBytes(salt);

			// Return the string encoded salt
			Convert.ToBase64String(salt);

			return Hash(textoPlano, salt);
		}

		public ResultadoHash Hash(string textoPlano, byte[] salt)
		{
			var llaveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
				salt: salt, prf: KeyDerivationPrf.HMACSHA512,
				iterationCount: 10000,
				numBytesRequested: 32);
			var hash = Convert.ToBase64String(llaveDerivada);

			return new ResultadoHash()
			{
				Hash = hash,
				Salt = salt
			};
		}
	}
}
