using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarefaSpotfyApi.Context;

using TarefaSpotfyApi.Models;

namespace TarefaSpotfyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotfyController : ControllerBase
    {
        private readonly SpotfyContext _context;

        public SpotfyController(SpotfyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Spotfy spotfy)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Add(spotfy);
                _context.SaveChanges();

                return CreatedAtAction(nameof(ObterPorId), new { id = spotfy.Id }, spotfy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar música: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var spotfy = _context.Spotfys.Find(id);
            if (spotfy == null)
            {
                return NotFound("Música não encontrada.");
            }

            return Ok(spotfy);
        }

        [HttpGet("obterPorNome/{musica}")]
        public IActionResult ObterPorNome(string musica)
        {
            var resultados = _context.Spotfys
                .Where(x => x.Musica.Contains(musica))
                .ToList();

            if (resultados == null || resultados.Count == 0)
            {
                return NotFound("Nenhuma música encontrada com esse nome.");
            }

            return Ok(resultados);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Spotfy spotfy)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var spotfyBanco = _context.Spotfys.Find(id);
            if (spotfyBanco == null)
            {
                return NotFound("Música não encontrada.");
            }

            spotfyBanco.Musica = spotfy.Musica;
            spotfyBanco.Album = spotfy.Album;
            spotfyBanco.Artista = spotfy.Artista;

            try
            {
                _context.Spotfys.Update(spotfyBanco);
                _context.SaveChanges();
                return Ok(spotfyBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar música: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult ObterTodas()
        {
            var musicas = _context.Spotfys.ToList();

            if (musicas == null || musicas.Count == 0)
            {
                return NotFound("Nenhuma música encontrada.");
            }

             return Ok(musicas);
        }


    }
}
