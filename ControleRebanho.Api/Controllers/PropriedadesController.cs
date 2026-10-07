using ControleRebanho.Api.Data;
using ControleRebanho.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleRebanho.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropriedadesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropriedadesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Propriedade>>> Listar()
        {
            return await _context.Propriedades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Propriedade>> BuscarPorId(int id)
        {
            var propriedade = await _context.Propriedades.FindAsync(id);
            if (propriedade == null) return NotFound();
            return propriedade;
        }

        [HttpPost]
        public async Task<ActionResult<Propriedade>> Criar(Propriedade propriedade)
        {
            var produtorExiste = await _context.Produtores.AnyAsync(p => p.Id == propriedade.ProdutorId);
            if (!produtorExiste) return BadRequest("Produtor não encontrado.");

            _context.Propriedades.Add(propriedade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BuscarPorId), new { id = propriedade.Id }, propriedade);
        }
    }
}