using ASoftwareVersaoFisioterapiaAPI.Context;

namespace ASoftwareVersaoFisioterapiaAPI.Services.TimeControl
{
    public class TimeControlService : ITimeControlService
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;

        public TimeControlService(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        public DateTime Dateformat(DateTime datetime)
        {
            return DateTime.Now;
        }

        public bool ValidateTimeControl(DateTime dateTime)
        {
            return _context.Clientes.Any(cliente => cliente.DataDaConsulta == dateTime);
        }
    }
}