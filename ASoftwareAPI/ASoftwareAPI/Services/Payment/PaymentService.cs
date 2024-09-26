using ASoftwareVersaoFisioterapiaAPI.Context;

namespace ASoftwareVersaoFisioterapiaAPI.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;
        private const string _session = "Sessão";

        public PaymentService(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        public float TotalValue(float value, int numberOfSessions)
        {
            return value * numberOfSessions;
        }

        private static float TotalValueBySession(float value, float discount, int numberOfSessions)
        {
            return (value * discount) * numberOfSessions;
        }

        public bool IsCategoryBySerrion(string type)
        {
            return type.Equals(_session);
        }

        public float Payment(string type, float value, float discount, int numberOfSessions)
        {
            if (IsCategoryBySerrion(type))
            {
                return TotalValue(value, numberOfSessions);
            }
            return TotalValueBySession(value, discount, numberOfSessions);
        }
    }
}