using authentication.domain.Commands;

namespace authentication.Domain.Commands
{
    public class AuthenticateCommand : BaseCPFCommand
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}