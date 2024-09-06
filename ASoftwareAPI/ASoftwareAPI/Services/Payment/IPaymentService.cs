namespace ASoftwareVersaoFisioterapiaAPI.Services.Payment
{
    public interface IPaymentService
    {
        float PaymentBySession(float value, int numberOfSessions);
        float PaymentByMonth(float value, float discount, int numberOfSessions);
        string IsCategoryBySerrionOrMonth(string type);
    }
}