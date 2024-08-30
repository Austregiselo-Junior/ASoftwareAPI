using ASoftwareVersaoFisioterapiaAPI.Context;

namespace ASoftwareVersaoFisioterapiaAPI.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ASoftwareVersaoFisioterapiaAPIContext _context;

        public AuthService(ASoftwareVersaoFisioterapiaAPIContext context)
        {
            _context = context;
        }

        public bool ValidateUserAsync(string login, string senha)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Login == login);

            if (user == null)
                return false;

            return user.Senha.ToLower().Equals(senha);
        }
    }
}