﻿using System.ComponentModel.DataAnnotations;

namespace ConexionBaseDatos.BaseDatos.DTO.Cuentas
{
	public class CredencialesRegister
	{
		[Required]
		public int id_perfil { get; set; }

		[Required]
		[EmailAddress]
		public string email { get; set; }
		[Required]
		public string user	{ get; set; }
		[Required]
		public string pass { get; set; }

	}
}