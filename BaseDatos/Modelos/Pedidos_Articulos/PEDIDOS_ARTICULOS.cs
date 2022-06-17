namespace ConexionBaseDatos.BaseDatos
{
	public class PEDIDOS_ARTICULOS
	{
		public int ID_PEDIDO_ARTICULO { get; set; }
		public int ID_ARTICULO { get; set; }
		public string DESCRIPCION { get; set; }
		public char TALLA { get; set; }
		public string COLOR { get; set; }
		public int CANTIDAD { get; set; }
		public Nullable<decimal> PRECIO_UNIDAD { get; set; }
		public Nullable<decimal> PRECIO	{ get; set; }
	}
}
