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

        public bool ValidateTimeControl(DateTime dateTime)
        {
            var dateTimeDB = _context.Clientes.Any(cliente => cliente.DataDaConsulta == dateTime);

            if (dateTimeDB)
                return false;

            return true;
        }
    }
}