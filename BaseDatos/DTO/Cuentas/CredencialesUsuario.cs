using System.ComponentModel.DataAnnotations;

namespace ConexionBaseDatos.BaseDatos.DTO.Cuentas
{
	public class CredencialesUsuario
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
