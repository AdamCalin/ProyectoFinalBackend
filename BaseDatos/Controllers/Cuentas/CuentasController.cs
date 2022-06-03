using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;
using ConexionBaseDatos.BaseDatos.DTO.Cuentas;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NEVER.BaseDatos.Exceptions;
using NEVER.BaseDatos.Servicios.Cuentas;
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
		private readonly HashService hashService;
		private readonly CuentasDbContext _context;
		private readonly IDataProtector dataProtector;

		public CuentasController(UserManager<IdentityUser> userManager, 
		IConfiguration configuration, 
		SignInManager<IdentityUser> signInManager,
		CuentasDbContext context, 
		ICuentasService service,
		IDataProtectionProvider dataProtectionProvider)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.signInManager = signInManager;
			this._service = service;
			this._context = context;
		}



		[HttpPost("registrar")]
		public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesRegister register)
		{
				var respuesta = _service.PostRegister(register);

				if (respuesta.retCode == 0)
				{
					return Ok(ConstruirTokenRegister(respuesta));
				}
				else
				{
					throw new Exception("CuentasController.HttpPost.Registrar." + respuesta.mensaje);
				}
		}



		[HttpPost("login")]
		public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesLogin login)
		{
		
				var respuesta = _service.PostLogin(login);

			if (respuesta.retCode == 0)
			{
				return Ok(ConstruirTokenLogin(respuesta));
			}
			else
			{
				throw new WrongCredentialsException(respuesta.mensaje);
			}



		}
		
		[HttpGet("RenovarToken")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		public RespuestaAutenticacion Renovar()
		{
			try
			{
				var idUsuarioClaim = HttpContext.User.Claims.Where(claim => claim.Type == "idUsuario").FirstOrDefault();
				var idUsuario = idUsuarioClaim.Value;
				var credencialesRenovacion = new CredencialesLogin()
				{
					user = idUsuario
				};

				return RenovarToken(credencialesRenovacion);
			}
			catch (Exception ex)
			{
				throw new Exception("CuentasController.HttpGet.RenovarToken", ex);
			}
			
		}


		private Task<RespuestaAutenticacion> ConstruirTokenLogin(LoginResponseDTO credencialesLogin)
		{
			var claims = new List<Claim>()
				{
					new Claim("idUsuario", credencialesLogin.idUsuario.ToString())
				};
			var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

			var expiracion = DateTime.UtcNow.AddYears(1);

			var securityToken = new JwtSecurityToken(
									issuer: null,
									audience: null,
									claims: claims,
									expires: expiracion,
									signingCredentials: creds
								);
			return Task.FromResult(
										new RespuestaAutenticacion()
										{
											Id_usuario = credencialesLogin.idUsuario,
											Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
											Expiracion = expiracion
										}
			);
		}

		private Task<RespuestaAutenticacion> ConstruirTokenRegister(LoginResponseDTO credencialesRegister)
		{

			var claims = new List<Claim>()
				{
					new Claim("idUsuario", credencialesRegister.idUsuario.ToString())
				};
			var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

			var expiracion = DateTime.UtcNow.AddYears(1);

			var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

			return Task.FromResult(
													new RespuestaAutenticacion()
													{
														Id_usuario = credencialesRegister.idUsuario,
														Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
														Expiracion = expiracion
													}
						);
		}
		private RespuestaAutenticacion RenovarToken(CredencialesLogin credencialesLogin)
		{
			var claims = new List<Claim>()
				{
					new Claim("user", credencialesLogin.user)
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
