using ConexionBaseDatos;
using ConexionBaseDatos.BaseDatos.Articulos.Base_Datos;
using ConexionBaseDatos.BaseDatos.Direcciones.Base_Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//CONEXION CON LA BBDD DE CADA APARTADO 
builder.Services.AddDbContext<ArticuloDbContext>(options => options.UseSqlServer( builder.Configuration.GetConnectionString("defaultConnection") ));
builder.Services.AddDbContext<DireccionesDbContext>(options => options.UseSqlServer( builder.Configuration.GetConnectionString("defaultConnection") ));
builder.Services.AddDbContext<UsuarioDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddDbContext<StockDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));



//SERVICIOS
builder.Services.AddScoped<IArticuloService, ArticulosService>();
builder.Services.AddScoped<IDireccionesService, DireccionesService>();
builder.Services.AddScoped<IUsuarioService, UsuariosService>();
builder.Services.AddScoped<IStockService, StockService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
