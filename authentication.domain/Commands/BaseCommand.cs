using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;

namespace authentication.domain.Commands
{
    public interface ICommand { bool Validate(); }

    public abstract class BaseCommand : Notifiable<Notification>, ICommand
    {
        [JsonIgnore] public Guid RequestPersonId { get; set; } = Guid.Empty;
        // public string UsersApiEmail { get; set; } = string.Empty;

        [JsonIgnore] public DateTime RequestTime { get; set; } = DateTime.UtcNow;


        public virtual bool Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires().IsTrue(RequestPersonId != Guid.Empty, "RequestPersonId is required")
            );
            Console.WriteLine("BaseCommand.Validate()");
            return IsValid;
        }
    }
    public class BaseCPFCommand : BaseCommand
    {
        public string? CPF { get; set; } = null;

        public override bool Validate()
        {
            base.Validate();

            if (CPF != null) CPF = CPF.Replace(".", "").Replace("-", "");

            AddNotifications(new Contract<CreatePersonCommand>().Requires()
                .IsTrue(MyUtils.Document.IsCPF(CPF), "CPF", "O CPF está inválido ou em branco")
            );

            return IsValid;
        }
    }

    public class BaseCNPJCommand : BaseCommand
    {
        public string? CNPJ { get; set; } = null;

        public override bool Validate()
        {
            base.Validate();

            if (CNPJ != null) CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            AddNotifications(new Contract<CreatePersonCommand>().Requires()
                .IsTrue(MyUtils.Document.IsCNPJ(CNPJ), "CNPJ", "O CNPJ está inválido ou em branco")
            );

            return IsValid;
        }
    }

    public class BaseCPFandCNPJCommand : BaseCommand
    {
        public string? CPF { get; set; } = null;
        public string? CNPJ { get; set; } = null;
        public Guid PersonId { get; set; } = Guid.Empty;
        public Guid CompanyId { get; set; } = Guid.Empty;

        public override bool Validate()
        {
            base.Validate();

            if (CPF != null) CPF = CPF.Replace(".", "").Replace("-", "");

            if (CNPJ != null) CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            AddNotifications(new Contract<CreatePersonCommand>().Requires()
                .IsTrue(MyUtils.Document.IsCPF(CPF) || PersonId != Guid.Empty, "CPF", "O CPF está inválido ou em branco")
                .IsTrue(MyUtils.Document.IsCNPJ(CNPJ) || PersonId != Guid.Empty, "CNPJ", "O CNPJ está inválido ou em branco")
            );

            return IsValid;
        }

        public bool CustomValidate()
        {
            base.Validate();

            if (CPF != null) CPF = CPF.Replace(".", "").Replace("-", "");

            if (CNPJ != null) CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            AddNotifications(new Contract<CreatePersonCommand>().Requires()
                .IsTrue(MyUtils.Document.IsCPF(CPF), "CPF", "O CPF está inválido ou em branco")
                .IsTrue(MyUtils.Document.IsCNPJ(CNPJ), "CNPJ", "O CNPJ está inválido ou em branco")
            );

            return IsValid;
        }
    }
}