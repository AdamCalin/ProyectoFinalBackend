namespace ConexionBaseDatos.BaseDatos
{
	public class PEDIDOS
	{
		public int ID_PEDIDO { get; set; }
		public int ID_ARTICULO { get; set; }
		public int CANTIDAD { get; set; }
		public Nullable<decimal> PRECIO	{ get; set; }
	}
}
