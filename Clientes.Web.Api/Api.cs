using Clientes.Web.Api.Models;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Web.Api
{
    
    public class Api
    {
        public static async Task<IResult> ObtenerClientePorId(ClientesContext context, int id)
        {
            try
            {
                var cliente = await context.Clientes.FindAsync(id);
                if (cliente == null) return Results.NotFound();

                return Results.Ok(cliente);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


        public static async Task<IResult> ObtenerClientePorCuil(ClientesContext context, string cuil)
        {
            try
            {
                var cliente = await context.Clientes.Where(x => x.Cuil == cuil).FirstOrDefaultAsync();
                if (cliente == null) return Results.NotFound();

                return Results.Ok(cliente);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


        public static async Task<IResult> AgregarCliente(ClientesContext context, Cliente cliente)
        {
            try
            {
                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Results.Problem($"{ex.Message} \nInnerException: {ex?.InnerException.Message}");
            }

            /********* Notifica al servicio de auditoria de forma asincronica **********/
            try
            {
                string kafkaUrl = GetFromConfig<string>("Kafka:Url");
                var config = new ProducerConfig { BootstrapServers = kafkaUrl, Acks = Acks.Leader };
                using var producer = new ProducerBuilder<Null, string>(config).Build();
                
                var response = await producer.ProduceAsync("auditoria-clientes",
                        new Message<Null, string> { Value = $"Cuit: {cliente.Cuil} | Nombre: {cliente.Nombre}" });
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine(ex.Message);
            } /*************************************************************************/

            return Results.Created($"/clientes/{cliente.Id}", cliente);
        }


        public static async Task<IResult> ActualizarCliente(ClientesContext context, Cliente cliente)
        {
            try
            {
                bool clienteEncontrado = await context.Clientes.AnyAsync(x => x.Id == cliente.Id);
                if (!clienteEncontrado) return Results.NotFound();

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


        public static async Task<IResult> EliminarCliente(ClientesContext context, int id)
        {
            try
            {
                var cliente = await context.Clientes.FindAsync(id);
                if (cliente == null) return Results.NotFound();

                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


        private static T GetFromConfig<T>(string clave, T defaultValue = default)
        {
            using IHost host = Host.CreateDefaultBuilder().Build();
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
            return config.GetValue<T>(clave, defaultValue);
        }
    }
    
}
