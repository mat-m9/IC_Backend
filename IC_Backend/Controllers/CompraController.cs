using IC_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static IC_Backend.ApiRoutes;

namespace IC_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly DatabaseContext context;
        public CompraController(DatabaseContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Compra>>> Get()
        {
            var compras = await context.Compras
                .Include(p => p.producto)
                .ToListAsync();
            if (!compras.Any())
                return NotFound();
            return compras;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Compra>>> GetCompra(string id)
        {
            var compra = await context.Compras.Where(e => e.Id.Equals(id))
                .Include(p => p.producto)
                .FirstOrDefaultAsync();
            if (compra == null)
                return NotFound();
            return Ok(compra);
        }

        [HttpGet(template: ApiRoutes.CompraRuta.Vendedor)]
        public async Task<ActionResult<ICollection<Compra>>> GetProductoUsuarioVendedor(string idUsuario)
        {
            var compra = await context.Compras.Where(e => e.usuarioVentaId.Equals(idUsuario))
                .Include(p => p.producto)
                .Include(u => u.usuarioCompra)
                .ToListAsync();
            if (compra == null)
                return NotFound();
            return Ok(compra);
        }

        [HttpGet(template: ApiRoutes.CompraRuta.Comprador)]
        public async Task<ActionResult<ICollection<Compra>>> GetProductoUsuarioComprador(string idUsuario)
        {
            var compra = await context.Compras.Where(e => e.usuarioCompraId.Equals(idUsuario))
                .Include(p => p.producto)
                .ToListAsync();
            if (compra == null)
                return NotFound();
            return Ok(compra);
        }



        [HttpPost]
        public async Task<ActionResult<string>> Post(Compra compra)
        {
            compra.fecha = DateTime.UtcNow;
            var created = context.Compras.Add(compra);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetCompra", new { id = compra.Id }, created.Entity);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Compra compra)
        {
            var existe = await Existe(compra.Id);

            if (!existe)
                return NotFound();

            context.Compras.Update(compra);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();


            var compra = await context.Compras.FindAsync(id);
            context.Compras.Remove(compra);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Compras.AnyAsync(p => p.Id == id);
        }
    }
}
