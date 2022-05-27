

namespace ConexionBaseDatos.BaseDatos
{
	public class ARTICULOS
	{
		public int ID_ARTICULO { get; set; }
		public string DESCRIPCION { get; set; }
		public string FABRICANTE { get; set; }
		public Nullable<int> PESO { get; set; }
		public Nullable<int> ALTO { get; set; }
		public Nullable<int> LARGO { get; set; }
		public Nullable<int> ANCHO { get; set; }
		public Nullable<decimal> PRECIO { get; set; }
		public char TALLA	{ get; set; }
		public string COLOR { get; set; }
		public string N_REGISTRO { get; set; }
	}
}
