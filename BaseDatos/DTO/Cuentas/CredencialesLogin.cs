using System.ComponentModel.DataAnnotations;

namespace ConexionBaseDatos.BaseDatos.DTO.Cuentas
{
	public class CredencialesLogin
	{
		[Required]
		public string user	{ get; set; }
		[Required]
		public string pass { get; set; }

	}
}
