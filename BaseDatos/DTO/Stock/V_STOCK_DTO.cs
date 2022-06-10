namespace ConexionBaseDatos.DTOs
{
	public class V_STOCK_DTO
	{
		public string descripcion { get; set; }
		public string color { get; set; }
		public char talla { get; set; }
		public int cantidad_stock { get; set; }
		public int cantidad_pedido { get; set; }
		public int cantidad_envio { get; set; }
	}
}
