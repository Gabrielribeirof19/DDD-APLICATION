namespace authentication.domain.Commands
{
    public class CreatePersonCommand : BaseCPFCommand
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<Entities.Address> Addresses { get; set; }

        public List<Entities.Email> Emails { get; set; }
    }
}