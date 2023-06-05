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
        private readonly ILogger<TransferenciasController> _logger;
        static readonly HttpClient client = new HttpClient();

        public TransferenciasController(TransferenciasContext context, ILogger<TransferenciasController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("{cbuOrigen}")]
        public async Task<ActionResult<List<Transferencia>>> Get(string cbuOrigen)
        {
            try
            {
                _logger.LogInformation($"Solicitando lista de transferencias del CBU {cbuOrigen}");
                var transferencias = await _context.Transferencias.Where(x => x.CbuOrigen == cbuOrigen).ToListAsync();
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al realizar Post de Transferencia", ex);
                return Conflict(ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Transferencia transferencia)
        {
            try
            {
                _logger.LogInformation($"Solicitando transferencias del CUIL {transferencia.CuilOriginante} a {transferencia.CuilDestinatario}");

                /************ Llamado a servicio de clientes ***************/
                var result = await client.GetAsync($"http://localhost:5181/clientes/cuil/{transferencia.CuilOriginante}");
                
                if (result.StatusCode == HttpStatusCode.NotFound)
                    return BadRequest($"Cuil del originante {transferencia.CuilOriginante} no fue hallado por el serivico de clientes");

                if (!result.IsSuccessStatusCode)
                    return Conflict("Error al invocar servicio de Clientes");
                /**********************************************************/

                var nuevaTransferencia = await _context.Transferencias.AddAsync(transferencia);
                await _context.SaveChangesAsync();
                return Created($"/clientes/{transferencia.Id}", transferencia);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al realizar Post de Transferencia", ex);
                return Conflict(ex);
            }
        }

    }
}
