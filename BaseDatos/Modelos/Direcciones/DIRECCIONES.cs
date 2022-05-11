namespace ConexionBaseDatos.BaseDatos
{
	public class DIRECCIONES
	{
		public int ID_DIRECCION { get; set;}
		public int		ID_USUARIO		{ get; set; }
		public string	CALLE			{ get; set; }
		public string	PROVINCIA		{ get; set; }
		public string	POBLACION		{ get; set; }
		public int		CODIGO_POSTAL	{ get; set; }
		public int		NUMERO			{ get; set; }
		public int		PISO			{ get; set; }	
		public char		PUERTA			{ get; set; }
		public string	PERSONA_CONTACTO { get; set; }
		public string	TELEFONO		{ get; set; }

 	}
}
