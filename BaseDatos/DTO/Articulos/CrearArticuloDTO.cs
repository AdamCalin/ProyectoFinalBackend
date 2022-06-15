
namespace ConexionBaseDatos.DTOs
{
	public class CrearArticuloDTO
	{
		public string descripcion { get; set; }
		public string fabricante { get; set; }
		public int peso { get; set; }
		public int largo { get; set; }
		public int ancho  { get; set; }
		public int alto { get; set; }
		public decimal precio { get; set; }
		public char talla { get; set; }
		public string color { get; set; }
		public string n_registro { get; set; }

		public string imagen { get; set; }
		public char sexo { get; set; }
	}
}
