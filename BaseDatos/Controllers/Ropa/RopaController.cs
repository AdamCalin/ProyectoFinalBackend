using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Ropa.Base_Datos;
using Microsoft.AspNetCore.Mvc;
using NEVER.BaseDatos.DTO.Ropa;

namespace ConexionBaseDatos.Controllers
{ 
		[ApiController]
		[Route("api/ropa")]
		public class RopaController : ControllerBase
		{

			public readonly IRopaService _service;
			private readonly RopaDbContext _context;

			public RopaController(IRopaService service, RopaDbContext context)
			{
				_service = service;
				_context = context;
			}
		[HttpGet("{id_articulo:int}")]
			public List<ROPA> Get(int id_articulo)
			{
				return _service.GetRopaId(id_articulo);
			}
		[HttpDelete("{id_ropa:int}")]
			public ResponseRopa  Delete(int id_ropa)
			{
					return  _service.BorrarRopa(id_ropa);


			}
        [HttpGet]
        public List<ROPA> Get(string color, int id_articulo)
        {
            try
            {
                return _service.GetRopaCarrito(id_articulo, color);


            }
            catch (Exception ex)
            {
                throw new Exception("RopaController.HttpGet.TryCatch", ex);
            }
        }


    }
}
