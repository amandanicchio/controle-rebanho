using ControleRebanho.Api.Data;
using ControleRebanho.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produtor>>> Listar()
        {
            return await _context.Produtores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produtor>> BuscarPorId(int id)
        {
            var produtor = await _context.Produtores.FindAsync(id);
            if (produtor == null) return NotFound();
            return produtor;
        }

        [HttpPost]
        public async Task<ActionResult<Produtor>> Criar(Produtor produtor)
        {
            _context.Produtores.Add(produtor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BuscarPorId), new { id = produtor.Id }, produtor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Produtor produtor)
        {
            if (id != produtor.Id) return BadRequest();

            var existe = await _context.Produtores.AnyAsync(p => p.Id == id);
            if (!existe) return NotFound();

            _context.Entry(produtor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var produtor = await _context.Produtores.FindAsync(id);
            if (produtor == null) return NotFound();

            _context.Produtores.Remove(produtor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}