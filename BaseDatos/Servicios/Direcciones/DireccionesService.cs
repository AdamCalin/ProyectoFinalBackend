using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos;
using ConexionBaseDatos.DTOs;

namespace ConexionBaseDatos
{

	public interface IDireccionesService
	{
		List<DIRECCIONES> GetDireccion();
		public string PostDireccion(CrearDireccionDTO direccion);
	}

	public class DireccionesService : IDireccionesService
	{
		public DireccionesDbContext _context;
		public DireccionesService(DireccionesDbContext context)
		{
			this._context = context;
		}


		public List<DIRECCIONES> GetDireccion()
		{
			return _context.DIRECCIONES.ToList();
		}

		public string PostDireccion(CrearDireccionDTO direccion)
		{
			var mensaje = "";
			var retCode = 0;

			_context.PaCrearDireccion(direccion.id_usuario, direccion.calle, direccion.provincia, direccion.poblacion, direccion.codigo_postal, direccion.numero, direccion.piso, direccion.puerta, direccion.persona_contacto, direccion.telefono, out mensaje, out retCode);

			return "Direccion agregada correctamente";
		}
	}
}
