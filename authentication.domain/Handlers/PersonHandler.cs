using authentication.domain.Commands;
using authentication.domain.entities;
using authentication.domain.ValueObjects;
using authentication.Domain.Handlers;
using authentication.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace authentication.domain.Handlers
{
    public class PersonHandler : BaseHandler
    {
        public PersonHandler(ILogger<PersonHandler> logger, IPersonRepository personRepository) : base(logger)
        {
            _personRepository = personRepository ?? throw new SystemException("Person repository not found");
        }
        private readonly IPersonRepository _personRepository;

        public async Task<ICommandResult> Handle(CreatePersonCommand command)
        {
            try
            {
                Console.WriteLine("entrei no handler");
                if (!command.Validate()) return new BadRequestCommandResult(command.Notifications, "Comando inválido");
                Console.WriteLine("passei da primeira validacao");
                Person? person = await _personRepository.GetByCpf(command.CPF!);
                Console.WriteLine("person: " + person);
                if (person != null) return new BadRequestCommandResult(null, "Esse CPF já está em uso na plataforma.");

                person = new Person();
                person.Password = new Password();
                person.Password.GenerateSalt();



                await _personRepository.Create(person);

                person.Password = null;
                Console.WriteLine("passei aqui");
                return new SuccessCommandResult(person, "Person created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating person");
                return new ExceptionCommandResult(ex);
            }
        }
        public async Task<ICommandResult> Handle(CreatePersonEmailCommand command)
        {
            try
            {
                if (!command.Validate()) return new BadRequestCommandResult(command.Notifications, "Comando inválido");

                Person? person = await _personRepository.GetByCpf(command.CPF!);

                if (person == null) return new BadRequestCommandResult(null, "Esse CPF não está cadastrado na plataforma.");

                if (command.Emails == null) return new BadRequestCommandResult(null, "Email inválido");

                int added = 0;

                foreach (var email in command.Emails)
                {
                    if (person.Email.Find(x => x.Address == email.Address) != null) continue;

                    person.Email.Add(email);
                    added++;
                }
                if (added > 0)
                {
                    await _personRepository.Update(person);
                    return new SuccessCommandResult(person, "Emails created successfully");
                }
                else
                {
                    return new BadRequestCommandResult(null, "Emails já cadastrados");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating person");
                return new ExceptionCommandResult(ex);
            }
        }

    }
}
