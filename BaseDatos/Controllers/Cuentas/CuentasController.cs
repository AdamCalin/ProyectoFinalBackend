using ConexionBaseDatos.BaseDatos;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;
using ConexionBaseDatos.BaseDatos.DTO.Cuentas;
using ConexionBaseDatos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
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
		private readonly IDataProtector dataProtector;

		public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, CuentasDbContext context, ICuentasService service, IDataProtectionProvider dataProtectionProvider)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.signInManager = signInManager;
			this._service = service;
			this._context = context;
			dataProtector = dataProtectionProvider.CreateProtector("valor_secreto");
		}

		[HttpGet("encriptar")]
		public ActionResult Encriptar()
		{
			var textoPlano = "Adam Calin";
			var textoCifrado = dataProtector.Protect(textoPlano);
			var textoDesencriptado = dataProtector.Unprotect(textoCifrado);

			return Ok(new
			{
				textoPlano = textoPlano,
				textoCifrado = textoCifrado,
				textoDesencriptado = textoDesencriptado

			});
		}

		[HttpGet("hash/{textoPlano}")]
		public ActionResult RealizarHash(string textPlano)
		{
			var resultado1 = hashService.Hash(textoPlano);
			var resultado2 = hashService.Hash(textoPlano);

			return Ok(new
			{
				textPlano = textPlano,
				Hash1 = resultado1,
				Hash2 = resultado2
			});

		}

		[HttpPost("registrar")]
		public RespuestaAutenticacion Registrar(CredencialesRegister register)
		{
			try
			{
				var respuesta = _service.PostLogin(register);

				if (respuesta.retCode == 0)
				{
					return ConstruirTokenRegister(register);
				}
				else
				{
					throw new Exception("CuentasController.HttpPost.Registrar." + respuesta.mensaje);
				}

			}
			catch (Exception ex)
			{
				throw new Exception("CuentasController.HttpPost.Registrar.TryCatch", ex);
			}
		}



		[HttpPost("login")]
		public RespuestaAutenticacion Login(CredencialesLogin login)
		{
			try
			{
				var respuesta = _service.ComprobacionLogin(login);

				if (respuesta.retCode == 0)
				{
					return ConstruirTokenLogin(respuesta);
				}
				else
				{
					throw new Exception("CuentasController.HttpPost.Login." + respuesta.mensaje);
				}
			}catch (Exception ex)
			{
				throw new Exception("CuentasController.HttpPost.Login.TryCatch", ex);
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


		private RespuestaAutenticacion ConstruirTokenLogin(LoginResponseDTO credencialesLogin)
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
			return new RespuestaAutenticacion()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
				Expiracion = expiracion
			};
		}
		private RespuestaAutenticacion ConstruirTokenRegister(CredencialesRegister credencialesRegister)
		{
			var claims = new List<Claim>()
				{
					new Claim("email", credencialesRegister.email)
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
