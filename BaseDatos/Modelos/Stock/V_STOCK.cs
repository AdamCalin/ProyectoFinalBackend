
using System.ComponentModel.DataAnnotations;

namespace ConexionBaseDatos.BaseDatos
{
	public class V_STOCK
	{
		[Key]
		public int ID_ROPA { get; set; }
		public int ID_STOCK { get; set; }
		public int ID_ARTICULO { get; set; }
		public string? DESCRIPCION { get; set; }
		public string? COLOR { get; set; }
		public char? TALLA { get; set; }
		public int? CANTIDAD_STOCK { get; set; }
		public int? CANTIDAD_PEDIDO { get; set; }
		public int? CANTIDAD_ENVIO { get; set; }
	}
}
