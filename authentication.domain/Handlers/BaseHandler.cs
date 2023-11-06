using authentication.domain.Commands;
using Flunt.Notifications;
using Microsoft.Extensions.Logging;

namespace authentication.Domain.Handlers
{
    public interface IHandler<T> where T : domain.Commands.ICommand
    {
        Task<ICommandResult> Handle(T command);
    }

    public abstract class BaseHandler : Notifiable<Notification>
    {
        protected readonly ILogger _logger;
        protected BaseHandler(ILogger logger)
        {
            _logger = logger;
        }
    }
}