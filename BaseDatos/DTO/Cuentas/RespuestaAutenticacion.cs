namespace ConexionBaseDatos.BaseDatos.DTO.Cuentas
{
	public class RespuestaAutenticacion
	{
		public string Token { get; set; }
		public DateTime Expiracion { get; set; }
		public int Id_usuario { get; set; }
	}
}
