using System.ComponentModel.DataAnnotations.Schema;
using authentication.domain.entities;

namespace authentication.domain.Entities
{
    public class Email : Entity
    {
        [ForeignKey("PersonId")]

        public Guid PersonId { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsRecovery { get; set; }
        public string? Address { get; set; }

        public Domain.Enums.EMailType? Type { get; set; }
        public bool? Verified { get; set; }

        public Email() { }

        public Email(string address)
        {
            Address = address;
        }

        public Email(Email email)
        {
            Address = email.Address;
            IsPrimary = email.IsPrimary;
            IsRecovery = email.IsRecovery;
            Type = email.Type;
            Verified = email.Verified;

        }
    }
}