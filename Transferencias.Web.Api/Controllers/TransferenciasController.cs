using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Transferencias.Web.Api.Models;


namespace Transferencias.Controllers
{
    [Route("/Transferencias")]
    [ApiController]
    public class TransferenciasController : ControllerBase
    {
        private readonly TransferenciasContext _context;
        static readonly HttpClient client = new HttpClient();

        public TransferenciasController(TransferenciasContext context)
        {
            _context = context;
        }


        [HttpGet("{cbuOrigen}")]
        public async Task<ActionResult<List<Transferencia>>> Get(string cbuOrigen)
        {
            try
            {
                var transferencias = await _context.Transferencias.Where(x => x.CbuOrigen == cbuOrigen).ToListAsync();
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Transferencia transferencia)
        {
            try
            {
                /************ Llamado a servicio de clientes ***************/
                var result = await client.GetAsync($"http://localhost:5181/clientes/cuil/{transferencia.CuilOriginante}");
                
                if (result.StatusCode == HttpStatusCode.NotFound)
                    return BadRequest($"Cuil del originante {transferencia.CuilOriginante} no se encontró en base de clientes");

                if (!result.IsSuccessStatusCode)
                    return Conflict("Error al invocar servicio de Clientes");
                /**********************************************************/

                var nuevaTransferencia = await _context.Transferencias.AddAsync(transferencia);
                await _context.SaveChangesAsync();
                return Created($"/clientes/{transferencia.Id}", transferencia);
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }
        }

    }
}
