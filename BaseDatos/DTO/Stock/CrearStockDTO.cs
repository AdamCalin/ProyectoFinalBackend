namespace ConexionBaseDatos.DTOs
{
	public class CrearStockDTO
	{
	public int id_articulo { get; set; }
		public int cantidad_stock { get; set; }
		public int cantidad_pedido { get; set; }
		public int cantidad_envio { get; set; }
	}
}
