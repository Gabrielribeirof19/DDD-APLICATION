using authentication.domain.entities;

namespace authentication.domain.Entities
{
    public class Jwt : Entity
    {
        public Guid PersonId { get; private set; } = Guid.Empty;
        public bool Active { get; private set; } = false;
        public DateTime LastUsedDateTime { get; private set; } = DateTime.UtcNow;
        public DateTime ExpirationDateTime { get; private set; } = DateTime.UtcNow.AddDays(90);

        public Jwt() { }

        public Jwt(Guid personId, DateTime expirationDateTime, string? lastUsedIPAddress = null)//, Address address = null)
        {
            if (personId == Guid.Empty) throw new ArgumentException("Invalid Guid");

            if (expirationDateTime < DateTime.UtcNow) throw new ArgumentException("Invalid DateTime");

            Active = true;
            SetId(Guid.NewGuid());
            PersonId = personId;
            ExpirationDateTime = expirationDateTime;

        }
    }
}