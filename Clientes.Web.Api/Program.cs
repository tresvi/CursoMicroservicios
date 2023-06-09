using Clientes.Web.Api;
using Clientes.Web.Api.Models;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/**************** Agregado de log **********************/
Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
/*******************************************************/

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ClientesContext>(options => options.UseSqlServer(connectionString));

/************* Agregado de opentelemetry ***************/
string openTelemetryUri = builder.Configuration.GetValue<string>("OpenTelemetry:Url");

builder.Services.AddOpenTelemetryTracing(b => {
    b.SetResourceBuilder(
        ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
     .AddAspNetCoreInstrumentation()
     .AddOtlpExporter(opts => { opts.Endpoint = new Uri(openTelemetryUri); });
});
/*******************************************************/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//app.MapGet("/clientes/{algo}", (string algo) => $"El id es {algo}");
app.MapGet("/clientes", async (ClientesContext context) => await context.Clientes.ToListAsync());

app.MapGet("/clientes/{id}", Api.ObtenerClientePorId)
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError);

app.MapGet("/clientes/cuil/{cuil}", Api.ObtenerClientePorCuil);
//Otra manera pero con funcion lambda
//app.MapGet("/clientes/cuil/{cuil}", async (ClientesContext context, string cuil) => await context.Clientes.Where(x => x.Cuil == cuil).FirstOrDefaultAsync()).WithName("Api");
app.MapPost("/clientes", Api.AgregarCliente);
app.MapPut("/clientes/{id}", Api.ActualizarCliente);
app.MapDelete("/clientes/{id}", Api.EliminarCliente);

app.Run();


/*Add nugets:  Microsoft.EntityFrameworkCore.Tools,  Microsoft.EntityFrameworkCore.SqlServer
Desde Nuget Package Manager ejecutar: 
    NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0-preview.4.23259.3
    NuGet\Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0-preview.4.23259.3

Instruccion para crear entidades de la base en el codigo (correr desde la consola de paquetes nuget)
Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=Clientes; Integrated Security = True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Levantar Jaeger en Docker (desde Powershell):
docker run --name jaeger -p 13133:13133 -p 16686:16686 -p 4317:4317 -d --restart=unless-stopped jaegertracing/opentelemetry-all-in-one
*/

/* 
Docker:
    Compilacion de Imagen:  
        docker build -t clienteswebapi:latest ./ -f "Clientes.Web.Api//Dockerfile"
    Creacion del contenedor en base a la imagen:
        docker run -d -p 6020:80 clienteswebapi:latest

    Taggear imagen para poder subirla:
        docker tag clienteswebapi:latest tresvi/clienteswebapi:latest
    Hacer el push de la imagen al repositorio 
        docker push tresvi/clienteswebapi:latest
*/
