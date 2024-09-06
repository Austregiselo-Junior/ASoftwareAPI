using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using ASoftwareVersaoFisioterapiaAPI.Services.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASoftwareVersaoFisioterapiaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;
        private readonly IPaymentService _paymentService;

        public CategoriaController(ASoftwareVersaoFisioterapiaAPIContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        [HttpGet("TabelaControleFinanceiro")]
        public ActionResult<IEnumerable<object>> Get()
        {
            var CategoriaComClienteNome = _context.Categorias.AsNoTracking().Include(categoria => categoria.Clientes).Select(categoria => new
            {
                CategoriaNome = categoria.Nome,
                categoria.ValorDaSessao,
                categoria.QuantidadeDeSessao,
                categoria.ValorTotal,
                categoria.Desconto,
                categoria.ValorPago,
                categoria.Vencimento,
                categoria.SituacaoFinanceira,
                Clientes = categoria.Clientes.Select(cliente => new
                {
                    cliente.Nome
                })
            }).ToList();
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };
            var json = JsonSerializer.Serialize(CategoriaComClienteNome, options);

            return Content(json);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            return (categoria is null) ? NotFound("Categoria não encontrada.") : categoria;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria == null)
                return BadRequest();
            try
            {
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