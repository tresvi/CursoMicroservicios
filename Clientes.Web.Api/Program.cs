using Clientes.Web.Api;
using Clientes.Web.Api.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClientesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

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


//Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Clientes; Integrated Security = True; Connect Timeout = 30; Encrypt = False;

/*Add nugets:  Microsoft.EntityFrameworkCore.Tools,  Microsoft.EntityFrameworkCore.SqlServer
Desde Nuget Package Manager ejecutar: 
    NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0-preview.4.23259.3
    NuGet\Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0-preview.4.23259.3
*/
//Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=Clientes; Integrated Security = True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
