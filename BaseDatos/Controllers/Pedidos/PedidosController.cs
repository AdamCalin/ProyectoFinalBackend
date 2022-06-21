using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NEVER.BaseDatos.DTO.Pedidos;
using System.Net;

namespace ConexionBaseDatos.Controllers
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	[ApiController]
	[Route("api/pedidos")]
	public class Pedidos_ArticulosController : ControllerBase
	{

		public readonly IPedidosService _service;
		private readonly PedidosDbContext _context;

		public Pedidos_ArticulosController(IPedidosService service, PedidosDbContext context)
		{
			this._service = service;
			this._context = context;
		}

		[HttpGet]
		public ActionResult<List<PEDIDOS>> Get(string usuario)
		{
				return _service.GetPedido(usuario);

		}

		[HttpGet]
		[Route("/api/codigo")]
		public ActionResult<List<PEDIDOS>> GetCodigo(string codigo)
		{
			return _service.GetPedidoCodigo(codigo);

		}

		[HttpPost]
        [AllowAnonymous]
		public ResponsePedido Post(CrearPedidoDTO pedido)
		{
			
				return _service.PostPedido(pedido);
		
		}
        [HttpDelete]
		public ResponsePedido Delete(int id_pedido)
		{

			return _service.BorrarPedido(id_pedido);

		}
	}
}