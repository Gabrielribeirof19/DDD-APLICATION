using authentication.domain.Commands;

public class CreatePersonEmailCommand : BaseCPFCommand
{
    public List<authentication.domain.Entities.Email>? Emails { get; set; }

    public override bool Validate()
    {
        base.Validate();
        return IsValid;
    }
}
