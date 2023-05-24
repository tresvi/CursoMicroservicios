using Clientes.Web.Api;
using Clientes.Web.Api.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClientesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ClientesConnection")));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//app.MapGet("/clientes/{algo}", (string algo) => $"El id es {algo}");
app.MapGet("/clientes", async (ClientesContext context) => await context.Clientes.ToListAsync()).WithName("Api").WithName("API");
app.MapGet("/clientes/{id}", Api.ObtenerClientePorId)
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status500InternalServerError);
app.MapGet("/clientes/cuil/{cuil}", async (ClientesContext context, string cuil) => await context.Clientes.Where(x => x.Cuil == cuil).ToListAsync()).WithName("Api");
//app.MapGet("/clientes/cuit", Api.ObtenerClientePorCuil);
app.MapPost("/clientes", Api.AgregarCliente);
app.MapPut("/clientes/{id}", Api.ActualizarCliente);
app.MapDelete("/clientes/{id}", Api.EliminarCliente);

app.Run();


//Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Clientes; Integrated Security = True; Connect Timeout = 30; Encrypt = False;
//Add nugets:  Microsoft.EntityFrameworkCore.Tools,  Microsoft.EntityFrameworkCore.SqlServer
//Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=Clientes; Integrated Security = True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
