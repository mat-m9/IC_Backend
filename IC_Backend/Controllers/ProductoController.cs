using IC_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IC_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ProductoController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Producto>>> Get()
        {
            var productos = await context.Producto.ToListAsync();
            if (!productos.Any())
                return NotFound();
            return productos;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<AlertaDSPPP>>> GetAlerta(string id)
        {
            var alerta = await context.Alertas.Where(e => e.AlertaDSPPPId.Equals(id)).Include(r => r.ProcesoCompra).FirstOrDefaultAsync();
            if (alerta == null)
                return NotFound();
            return Ok(alerta);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(AlertaDSPPP alerta)
        {
            var created = context.Alertas.Add(alerta);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetAlerta", new { id = alerta.AlertaDSPPPId }, created.Entity);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(string id, Producto producto)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            alerta.AlertaDSPPPId = id;
            context.Alertas.Update(alerta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();


            var alerta = await context.Alertas.FindAsync(id);
            context.Alertas.Remove(alerta);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Alertas.AnyAsync(p => p.Id == id);
        }
    }
}
