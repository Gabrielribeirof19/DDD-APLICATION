using authentication.domain.entities;

namespace authentication.Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; } = string.Empty;
        public List<Person>? Users { get; set; } = new();
    }
}