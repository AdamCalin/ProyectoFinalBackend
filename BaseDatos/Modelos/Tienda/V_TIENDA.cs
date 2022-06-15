﻿using System.ComponentModel.DataAnnotations;

namespace ConexionBaseDatos.BaseDatos
{
	public class V_TIENDA
	{
		[Key]
		public int ID_ROPA { get; set; }
		public int ID_ARTICULO { get; set; }
		public string DESCRIPCION { get; set; }
		public decimal PRECIO { get; set; }
		public char TALLA { get; set; }
		public string COLOR { get; set; }
		public string IMAGEN { get; set; }
		public char SEXO { get; set; }

	}
}
