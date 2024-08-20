using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using Microsoft.AspNetCore.Http;
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
            return (usuario is null) ? NotFound("Produtos não encontrados.") : usuario;
        }

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> GetById(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.UsuarioId == id);
            return (usuario is null) ? NotFound("Produtos não encontrados.") : usuario;
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
    }
}