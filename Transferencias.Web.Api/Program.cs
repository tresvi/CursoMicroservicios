using Microsoft.EntityFrameworkCore;
using Transferencias.Web.Api.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TransferenciasContext>(options =>
    options.UseSqlServer(connectionString)
    );


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
