using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Transferencias.Web.Api.Models;

var builder = WebApplication.CreateBuilder(args);

/**************** Agregado de log **********************/
Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
/*******************************************************/

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TransferenciasContext>(options =>
    options.UseSqlServer(connectionString)
    );

/************* Agregado de opentelemetry ***************/
string openTelemetryUri = builder.Configuration.GetValue<string>("OpenTelemetry:Url");

builder.Services.AddOpenTelemetryTracing(b => {
    b.SetResourceBuilder(
        ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
     .AddAspNetCoreInstrumentation()
     .AddOtlpExporter(opts => { opts.Endpoint = new Uri(openTelemetryUri); });
});
/*******************************************************/

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();



//Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Clientes; Integrated Security = True; Connect Timeout = 30; Encrypt = False;

//Add nugets:  Microsoft.EntityFrameworkCore.Tools,  Microsoft.EntityFrameworkCore.SqlServer
//Desde Nuget Package Manager ejecutar: 
//  NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0-preview.4.23259.3
//  NuGet\Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0-preview.4.23259.3

//
//Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=Transferencias; Integrated Security = True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


/* 
Docker:
    Compilacion de Imagen:  
        docker build -t transferenciaswebapi:latest ./ -f "Transferencias.Web.Api//Dockerfile"
    Creacion del contenedor en base a la imagen:
        docker run -d -p 6010:80 transferenciaswebapi:latest

    Taggear imagen para poder subirla:
        docker tag transferenciaswebapi:latest tresvi/transferenciaswebapi:latest
    Hacer el push de la imagen al repositorio 
        docker push tresvi/transferenciaswebapi:latest
*/