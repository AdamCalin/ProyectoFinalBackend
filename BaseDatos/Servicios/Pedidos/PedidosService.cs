using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos;
using ConexionBaseDatos.DTOs;
using NEVER.BaseDatos.DTO.Pedidos;
using System.Net.Mail;

namespace ConexionBaseDatos
{
	public interface IPedidosService
	{
		List<PEDIDOS> GetPedido(string usuario);
		List<PEDIDOS> GetPedidoCodigo(string codigo);

		public ResponsePedido PostPedido(CrearPedidoDTO pedido);

		public ResponsePedido BorrarPedido(int id_pedido);
	}

	public class PedidosService : IPedidosService
	{
		public PedidosDbContext _context;
		public PedidosService(PedidosDbContext context)
		{
			this._context = context;
		}


		public List<PEDIDOS> GetPedido(string usuario)
		{
			return _context.PEDIDOS.Where(q => q.USUARIO == usuario).ToList();
		}
		public List<PEDIDOS> GetPedidoCodigo(string codigo)
		{
			return _context.PEDIDOS.Where(q => q.CODIGO == codigo).ToList();
		}
		public ResponsePedido PostPedido(CrearPedidoDTO pedido)
		{
			var mensaje = "";
			var retCode = 0;

			var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var Charsarr = new char[20];
			var random = new Random();

			for (int i = 0; i < Charsarr.Length; i++)
			{
				Charsarr[i] = characters[random.Next(characters.Length)];
			}

			var resultString = new String(Charsarr);
			pedido.codigo = resultString;


			_context.PaCrearPedido( pedido.usuario, pedido.codigo, pedido.estado, pedido.fecha, out mensaje, out retCode);

			ResponsePedido response = new ResponsePedido();

			response.mensaje = mensaje;
			response.retCode = retCode;

			return response;
		}
		public ResponsePedido BorrarPedido(int id_pedido)
		{
			var mensaje = "";
			var retCode = 0;

			var retorno = _context.PaBorrarPedido(id_pedido);
			ResponsePedido response = new ResponsePedido();

			response.mensaje = retorno.mensaje;
			response.retCode = retorno.retCode;

			return response;


		}
	}
}
