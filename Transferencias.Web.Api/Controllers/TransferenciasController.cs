using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
