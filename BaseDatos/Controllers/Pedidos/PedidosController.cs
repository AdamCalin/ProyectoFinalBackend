using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		public ActionResult<List<PEDIDOS>> Get()
		{
		

			try{
				return _service.GetPedido();
			}
			catch (Exception ex)
			{
				throw new Exception("PedidosController.HttpGet.TryCatch", ex);
			}
		}

		[HttpPost]
		public string Post(CrearPedidoDTO pedido)
		{
			

			try{
				return _service.PostPedido(pedido);
			}
			catch(Exception ex)
			{
				throw new Exception("PedidosController.HttpPost.TryCatch", ex);
			}
		}
	}
}