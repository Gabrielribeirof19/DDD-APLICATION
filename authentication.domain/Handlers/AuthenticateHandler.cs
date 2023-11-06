using System.Net;
using authentication.domain.Commands;
using authentication.domain.entities;
using authentication.Domain.Commands;
using authentication.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace authentication.Domain.Handlers
{
    public class AuthenticateHandler : BaseHandler
    {
        public AuthenticateHandler(ILogger<AuthenticateHandler> logger, IPersonRepository personRepository) : base(logger)
        {
            _personRepository = personRepository ?? throw new SystemException("Person repository not found");
        }
        private readonly IPersonRepository _personRepository;
        public async Task<ICommandResult> Handle(AuthenticateCommand command)
        {

            Person? person = await _personRepository.GetByEmail(command.Email);

            try
            {
                if (person == null) return new BadRequestCommandResult(null, "Usuario não encontrado");
            }
            catch (Exception ex)
            {
                return new NotFoundCommandResult(ex, "Usuario não encontrado");
            }
            if (person == null) return new BadRequestCommandResult(null, "Usuario não encontrado");

            if (person.Password == null || !person.Password.Check(command.Password)) return new BadRequestCommandResult(null, "Senha incorreta");


            try
            {
                if (person.Status == Domain.Enums.EStatus.Inactive) return new BadRequestCommandResult(null, "Usuario inativo");
            }
            catch (Exception ex)
            {
                return new NotFoundCommandResult(ex, "Usuario não encontrado");

            }




            try
            {
                if (!command.Validate()) return new BadRequestCommandResult(command.Notifications, "Comando inválido");
                var authenticate = new AuthenticateCommand
                {
                    RequestPersonId = person.Id,
                    Email = command.Email,
                    Roles = person.Roles.Select(x => x.Name).ToArray(),
                };

                return new SuccessCommandResult(authenticate, "Person authenticated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error authenticating person");
                return new CommandResult(HttpStatusCode.InternalServerError, false, "Error authenticating person", null, ex.Message, ex.InnerException?.Message);
            }
        }

    }
}