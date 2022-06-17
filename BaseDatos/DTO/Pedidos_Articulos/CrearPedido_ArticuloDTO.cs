namespace ConexionBaseDatos.DTOs
{
	public class CrearPedido_ArticuloDTO
	{
		public int id_articulo { get; set; }
		public string descripcion { get; set; }
		public char talla	{ get; set; }
		public string color { get; set; }
		public int cantidad { get; set; }
		public decimal precio_und { get; set; }
		public decimal precio { get; set; }
	}
}
