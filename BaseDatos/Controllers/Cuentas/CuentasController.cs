using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;
using ConexionBaseDatos.BaseDatos.DTO.Cuentas;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConexionBaseDatos.Controllers
{

	[ApiController]
	[Route("api/cuentas")]
	public class CuentasController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration configuration;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly ICuentasService _service;
		private readonly CuentasDbContext _context;

		public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, CuentasDbContext context, ICuentasService service)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.signInManager = signInManager;
			this._service = service;
			this._context = context;
		}


		[HttpPost("registrar")]
		public RespuestaAutenticacion Registrar(CredencialesUsuario login)
		{
			
			try{
				var respuesta =  _service.PostLogin(login);

				if (respuesta.retCode == 0)
				{
					return ConstruirToken(login);
				}
				else
				{
					throw new Exception(respuesta.mensaje);
				}
			}
			catch{
				throw new Exception("CuentasController.Registrar.TryCatch");
			}
		}



		[HttpPost("login")]
		public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
		{
			var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
			credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

			if (resultado.Succeeded)
			{
				return ConstruirToken(credencialesUsuario);
			}
			else
			{
				return BadRequest("Login incorrecto");
			}
		}
		private RespuestaAutenticacion ConstruirToken(CredencialesUsuario credencialesUsuario)
		{
			var claims = new List<Claim>()
				{
					new Claim("email", credencialesUsuario.Email)
				};
			var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

			var expiracion = DateTime.UtcNow.AddYears(1);

			var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

			return new RespuestaAutenticacion()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
				Expiracion = expiracion
			};
		}
	}

}
