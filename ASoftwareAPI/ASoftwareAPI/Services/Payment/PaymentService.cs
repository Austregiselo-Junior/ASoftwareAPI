using ASoftwareVersaoFisioterapiaAPI.Context;

namespace ASoftwareVersaoFisioterapiaAPI.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;
        private const string _session = "Session";
        private const string _month = "Mensal";

        public PaymentService(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        public float PaymentBySession(float value, int numberOfSessions)
        {
            return value * numberOfSessions;
        }

        public float PaymentByMonth(float value, float discount, int numberOfSessions)
        {
            return (value * discount) * numberOfSessions;
        }

        public string IsCategoryBySerrionOrMonth(string type)
        {
            return type.Equals(_session) ? _session : _month;
        }
    }
}