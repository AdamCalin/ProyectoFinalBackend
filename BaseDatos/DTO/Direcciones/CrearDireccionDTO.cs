namespace ConexionBaseDatos.DTOs
{
	public class CrearDireccionDTO
	{
		public int id_usuario { get; set; }
		public string calle { get; set; }
		public string provincia { get; set; }
		public string poblacion { get; set; }
		public int codigo_postal { get; set; }
		public int numero { get; set; }
		public int piso  { get; set; }
		public char puerta { get; set; }
		public string persona_contacto{ get; set; }
		public string telefono { get; set; }
	}
}
