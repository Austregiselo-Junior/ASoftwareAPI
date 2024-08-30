using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using ASoftwareVersaoFisioterapiaAPI.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASoftwareVersaoFisioterapiaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;
        private readonly IAuthService _authService;

        public UsuarioController(ASoftwareVersaoFisioterapiaAPIContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var usuario = _context.Usuarios.AsNoTracking().ToList();
            return (usuario is null) ? NotFound("Usuario não encontrados.") : usuario;
        }

        [HttpGet("Clientes")]
        public ActionResult<IEnumerable<Usuario>> GetClientesAndUsuario()
        {
            var usuarioseClientes = _context.Usuarios.AsNoTracking().Include(usuarios => usuarios.Clientes).ToList();
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };

            var json = JsonSerializer.Serialize(usuarioseClientes, options);
            return Content(json);
        }

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> GetById(int id)
        {
            var usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(usuario => usuario.UsuarioId == id);
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

        [HttpPost("login")]
        public IActionResult Login(string login, string senha)
        {
            if ((bool)(_authService?.ValidateUserAsync(login, senha)))
            {
                return Ok("Login realizado com sucesso.");
            }
            else
            {
                return Unauthorized("Credenciais inválidas.");
            }
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