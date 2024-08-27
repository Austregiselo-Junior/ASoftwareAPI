using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASoftwareVersaoFisioterapiaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;

        public ClienteController(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var cliente = _context.Clientes.ToList();
            return (cliente is null) ? NotFound("Cliente não encontrado.") : cliente;
        }

        [HttpGet("Categorias")]
        public ActionResult<IEnumerable<Cliente>> GetClientesAndCategorias()
        {
            var clientesECategorias = _context.Clientes.Include(clientes => clientes.Categoria).ToList();
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };
            var json = JsonSerializer.Serialize(clientesECategorias, options);
            return Content(json); ;
        }

        [HttpGet("{id:int}", Name = "ObterCliente")]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == id);
            return (cliente is null) ? NotFound("Cliente não encontrado.") : cliente;
        }

        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest();

                _context.Clientes.Add(cliente);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCliente", new { id = cliente.ClienteId }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error message: {ex.Message}," +
                    $" Inner exeption: {ex.InnerException}");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            try
            {
                if (id != cliente?.ClienteId)
                    return BadRequest("Id inválido");

                var clienteFromDB = _context?.Clientes.FirstOrDefault(c => c.ClienteId == id);

                if (clienteFromDB == null)
                    return BadRequest("Cliente não encontrado");

                _context.Entry(clienteFromDB).State = EntityState.Detached;
                _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error message: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var clienteFromDB = _context?.Clientes.FirstOrDefault(c => c.ClienteId == id);

            if (clienteFromDB == null)
                return NotFound("Cliente não encontrado");

            _context?.Clientes.Remove(clienteFromDB);
            _context?.SaveChanges();
            return Ok(clienteFromDB);
        }
    }
}