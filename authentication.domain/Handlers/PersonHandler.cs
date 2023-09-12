using System.Net;
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

                if (!command.Validate()) return new BadRequestCommandResult(command.Notifications, "Comando inválido");

                Person? person = await _personRepository.GetByCpf(command.CPF!);

                if (person != null) return new BadRequestCommandResult(null, "Esse CPF já está em uso na plataforma.");

                person = new Person
                {
                    Password = new()
                };
                person.Password.GenerateSalt();

                await _personRepository.Create(person);

                return new SuccessCommandResult(person, "Person created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating person");
                return new CommandResult(HttpStatusCode.InternalServerError, false, "Error creating person", null, ex.Message, ex.InnerException?.Message);
            }
        }
    }
}