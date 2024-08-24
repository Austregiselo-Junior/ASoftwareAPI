using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASoftwareVersaoFisioterapiaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;

        public UsuarioController(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var usuario = _context.Usuarios.ToList();
            return (usuario is null) ? NotFound("Usuario não encontrados.") : usuario;
        }

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> GetById(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.UsuarioId == id);
            return (usuario is null) ? NotFound("Usuario não encontrados.") : usuario;
        }

        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Usuario usuario)
        {
            if (id != usuario?.UsuarioId)
                return BadRequest("Id inválido");

            var usuarioFromDB = _context?.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuarioFromDB == null)
                return BadRequest("Categoria não encontrada");

            _context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(usuario);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var usuarioFromDB = _context?.Usuarios.FirstOrDefault(u => u.UsuarioId == id);

            if (usuarioFromDB == null)
                return NotFound("Usuario não encontrado");

            _context.Usuarios.Remove(usuarioFromDB);
            _context.SaveChanges();
            return Ok(usuarioFromDB);
        }
    }
}