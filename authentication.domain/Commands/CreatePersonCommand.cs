using authentication.domain.Entities;
using authentication.Domain.Entities;

namespace authentication.domain.Commands
{
    public class CreatePersonCommand : BaseCPFCommand
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public List<Email>? Email { get; set; }

        public string? SocialName { get; set; }

        public List<Address>? Addresses { get; set; }
        public List<Role>? Roles { get; set; }
    }
}