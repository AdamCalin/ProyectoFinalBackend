using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Tienda.Base_Datos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexionBaseDatos.Controllers
{ 
		[ApiController]
		[Route("api/tienda")]
		public class TiendaController : ControllerBase
		{

			public readonly ITiendaService _service;
			private readonly TiendaDbContext _context;

			public TiendaController(ITiendaService service, TiendaDbContext context)
			{
				this._service = service;
				this._context = context;
			}

			[HttpGet]
			public List<V_TIENDA> Get()
			{
				try
				{
					return _service.GetTienda();
				}
				catch (Exception ex)
				{
					throw new Exception("TiendaController.HttpGet.TryCatch", ex);
				}
			}
			[HttpGet]
			[Route("select")]
			public List<V_TIENDA> Get(string color, int id_articulo)
			{
				try
				{
					return _service.GetTiendaRopa(color, id_articulo);
				}
				catch (Exception ex)
				{
					throw new Exception("TiendaController.HttpGet.TryCatch", ex);
				}
			}
    }
}
