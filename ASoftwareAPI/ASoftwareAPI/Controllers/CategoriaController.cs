using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASoftwareVersaoFisioterapiaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;

        public CategoriaController(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categoria = _context.Categorias.ToList();
            return (categoria is null) ? NotFound("Categoria não encontrada.") : categoria;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            return (categoria is null) ? NotFound("Categoria não encontrada.") : categoria;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                    return BadRequest();

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error message: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria?.CategoriaId)
                return BadRequest("Id inválido");

            var categoriaFromDB = _context?.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoriaFromDB == null)
                return BadRequest("Categoria não encontrada");

            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoriaFromDB = _context?.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoriaFromDB == null)
                return NotFound("Categoria não encontrada");

            _context?.Categorias.Remove(categoriaFromDB);
            _context?.SaveChanges();
            return Ok(categoriaFromDB);
        }
    }
}