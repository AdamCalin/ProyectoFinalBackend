using ConexionBaseDatos;
using ConexionBaseDatos.BaseDatos.Articulos.Base_Datos;
using ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos;
using ConexionBaseDatos.BaseDatos.Usuarios.Base_Datos;
using ConexionBaseDatos.BaseDatos.Stock.Base_Datos;
using ConexionBaseDatos.BaseDatos.Pedidos.Base_Datos;
using Microsoft.EntityFrameworkCore;
using ConexionBaseDatos.BaseDatos.Pedidos_Articulo.Base_Datos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ConexionBaseDatos.BaseDatos.Cuentas.Base_Datos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//CONEXION CON LA BBDD DE CADA APARTADO 
builder.Services.AddDbContext<ArticuloDbContext>(options => options.UseSqlServer( builder.Configuration.GetConnectionString("defaultConnection") ));
builder.Services.AddDbContext<DireccionesDbContext>(options => options.UseSqlServer( builder.Configuration.GetConnectionString("defaultConnection") ));
builder.Services.AddDbContext<UsuarioDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddDbContext<StockDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddDbContext<PedidosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddDbContext<PedidosArticulosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddDbContext<CuentasDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));


//SERVICIOS
builder.Services.AddScoped<IArticuloService, ArticuloService>();
builder.Services.AddScoped<ICuentasService, CuentasService>();
builder.Services.AddScoped<IDireccionesService, DireccionesService>();
builder.Services.AddScoped<IUsuarioService, UsuariosService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IPedidosService, PedidosService>();
builder.Services.AddScoped<IPedidos_articulosService, Pedidos_ArticulosService>();

//JWT BEARER
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateActor = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	});
builder.Services.AddAuthorization();
 
//MAPERS


//IDENTIDADES
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ArticuloDbContext>().AddEntityFrameworkStores<CuentasDbContext>().AddDefaultTokenProviders();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Beearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{}
		}
	});
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
