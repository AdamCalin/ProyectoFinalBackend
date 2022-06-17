using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos_Articulo.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEVER.BaseDatos.DTO.Pedidos_Articulos;

namespace ConexionBaseDatos.Controllers
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ApiController]
	[Route("api/pedidos_articulos")]
	public class PedidosArticulosController : ControllerBase
	{

		public readonly IPedidos_articulosService _service;
		private readonly PedidosArticulosDbContext _context;

		public PedidosArticulosController(IPedidos_articulosService service, PedidosArticulosDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<PEDIDOS_ARTICULOS>> Get()
		{

			try{
				return _service.GetPedido_Articulo();
			}
			catch (Exception ex)
			{
				throw new Exception("Pedidos_ArticulosController.HttpGet.TryCatch", ex);
			}
		}

		[HttpPost]
		public ResponsePedidos_Articulos Post(CrearPedido_ArticuloDTO pedido)
		{

			try{
				return _service.CrearPedido_Articulo(pedido);
			}
			catch (Exception ex)
			{
				throw new Exception("Pedidos_ArticulosController.HttpPost.TryCatch", ex);
			}
		}
		[HttpDelete("{id_pedido_articulo:int}")]
		public async Task<ResponsePedidos_Articulos> Delete(int id_pedido_articulo)
		{
			try
			{
				return await _service.BorrarPedido_Articulo(id_pedido_articulo);


			}
			catch (Exception ex)
			{
				throw new Exception("UsuariosController.HttpGet.TryCatch", ex);
			}
		}

	}
}