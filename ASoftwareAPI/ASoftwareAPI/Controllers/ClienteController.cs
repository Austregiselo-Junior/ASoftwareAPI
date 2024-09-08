using ASoftwareVersaoFisioterapiaAPI.Context;
using ASoftwareVersaoFisioterapiaAPI.Model;
using ASoftwareVersaoFisioterapiaAPI.Services.Payment;
using ASoftwareVersaoFisioterapiaAPI.Services.TimeControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASoftwareVersaoFisioterapiaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;
        private readonly ITimeControlService _timeControlService;
        private readonly IPaymentService _paymentControlService;

        public ClienteController(ASoftwareVersaoFisioterapiaAPIContext context, ITimeControlService timeControlService, IPaymentService paymentControlService)
        {
            _context = context;
            _timeControlService = timeControlService;
            _paymentControlService = paymentControlService;
        }

        #region Gets

        [HttpGet("TabelaSimples")]
        public ActionResult<IEnumerable<object>> GetTime()
        {
            var cliente = _context.Clientes.AsNoTracking().Select(c => new { c.Nome, c.Telefone, c.DataDaConsulta }).ToList();

            return (cliente is null) ? NotFound("Cliente não encontrado.") : cliente;
        }

        [HttpGet("TabelaDetalhada")]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            var clientes = _context.Clientes.AsNoTracking().ToList();
            return (clientes is null) ? NotFound("Usuario não encontrados.") : clientes;
        }

        [HttpGet("{id:int}", Name = "ObterCliente")]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = _context.Clientes.AsNoTracking().FirstOrDefault(c => c.ClienteId == id);
            return (cliente is null) ? NotFound("Cliente não encontrado.") : cliente;
        }

        #endregion Gets

        [HttpPost("AdicionarCliente")]
        public ActionResult Post(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest();

                if (_timeControlService.ValidateTimeControl(cliente.DataDaConsulta))
                {
                    cliente.ValorTotal = _paymentControlService.TotalValue(cliente.ValorDaSessao, cliente.QuantidadeDeSessao);
                    cliente.ValorPago = _paymentControlService.Payment(cliente.Categoria, cliente.ValorDaSessao, cliente.Desconto, cliente.QuantidadeDeSessao);

                    _context.Clientes.Add(cliente);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest("Error message: Horário não disponível!");
                }

                return new CreatedAtRouteResult("ObterCliente", new { id = cliente.ClienteId }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error message: {ex.Message}," +
                    $" Inner exeption: {ex.InnerException}");
            }
        }

        [HttpPut("AtualizarCliente")]
        public ActionResult Put(string nome, Cliente cliente)
        {
            try
            {
                var hasclienteFromDB = _context?.Clientes.FirstOrDefault(c => c.Nome == nome);

                if (hasclienteFromDB == null)
                    return BadRequest("Cliente não encontrado");

                if (!_timeControlService.ValidateTimeControl(cliente.DataDaConsulta))
                {
                    return BadRequest("Error message: Horário não disponível!");
                }

                cliente.ValorTotal = _paymentControlService.TotalValue(cliente.ValorDaSessao, cliente.QuantidadeDeSessao);
                cliente.ValorPago = _paymentControlService.Payment(cliente.Categoria, cliente.ValorDaSessao, cliente.Desconto, cliente.QuantidadeDeSessao);

                _context.Entry(hasclienteFromDB).State = EntityState.Detached;
                _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChangesAsync();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error message: {ex.Message}");
            }
        }

        [HttpDelete("RemoverCliente")]
        public ActionResult Delete(string nome)
        {
            var clienteFromDB = _context?.Clientes.FirstOrDefault(c => c.Nome == nome);

            if (clienteFromDB == null)
                return NotFound("Cliente não encontrado");

            _context?.Clientes.Remove(clienteFromDB);
            _context?.SaveChanges();
            return Ok(clienteFromDB);
        }
    }
}