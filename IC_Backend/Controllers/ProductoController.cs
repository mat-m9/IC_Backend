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
            var productos = await context.Productos.ToListAsync();
            if (!productos.Any())
                return NotFound();
            return productos;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<Producto>>> GetProducto(string id)
        {
            var alerta = await context.Productos.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
            if (alerta == null)
                return NotFound();
            return Ok(alerta);
        }

        [HttpGet(template: ApiRoutes.Producto.IdUsuario)]
        public async Task<ActionResult<ICollection<Producto>>> GetProductoUsuario(string idUsuario)
        {
            var alerta = await context.Productos.Where(e => e.usuarioId.Equals(idUsuario)).ToListAsync();
            if (alerta == null)
                return NotFound();
            return Ok(alerta);
        }


        [HttpPost]
        public async Task<ActionResult<string>> Post(Producto producto)
        {
            var created = context.Productos.Add(producto);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProducto", new { id = producto.Id }, created.Entity);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Producto producto)
        {
            var existe = await Existe(producto.Id);

            if (!existe)
                return NotFound();

            context.Productos.Update(producto);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();


            var producto = await context.Productos.FindAsync(id);
            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Productos.AnyAsync(p => p.Id == id);
        }
    }
}
