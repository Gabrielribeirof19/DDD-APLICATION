using authentication.Domain.Entities;

namespace authentication.domain.entities
{
    public abstract class BasePerson : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string SocialName { get; set; } = string.Empty;

    }

    public class Person : BasePerson
    {
        public List<Entities.Address> Addresses { get; set; } = new();

        public ValueObjects.Password? Password { get; set; } = new();

        public List<Entities.Email> Email { get; set; } = new();
        public List<Role> Roles { get; set; } = new();

    }
}