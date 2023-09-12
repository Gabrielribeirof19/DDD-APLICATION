using System.Net;

namespace authentication.domain.Commands
{
    public interface ICommandResult
    {
        HttpStatusCode GetStatus();

        bool IsSuccess();

        string GetMessage();

        object? GetData();

        CommandResult GetResult();
    }

    public class CommandResult : ICommandResult
    {
        public CommandResult(HttpStatusCode httpStatusCode,
                    bool success,
                    string? message = null,
                    object? data = null,
                    string? exceptionMessage = null,
                    string? innerExceptionMessage = null)
        {
            HttpStatusCode = httpStatusCode;
            Success = success;
            Message = message ?? httpStatusCode.ToString();
            Data = data;
            ExceptionMessage = exceptionMessage;
            InnerExceptionMessage = innerExceptionMessage;

            if (ExceptionMessage != null)
            {
                Message += "<br /><br />__________<br />" + ExceptionMessage + "<br />";
            }

            if (InnerExceptionMessage != null)
            {
                Message += "<br /><br />__________<br />" + InnerExceptionMessage;
            }
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? InnerExceptionMessage { get; set; }

        public HttpStatusCode GetStatus() => this.HttpStatusCode;
        public bool Succeeded() => this.Success;
        public string GetMessage() => this.Message;
        public object? GetData() => this.Data;
        public CommandResult GetResult() => this;

        public bool IsSuccess()
        {
            throw new NotImplementedException();
        }
    }
    public class SuccessCommandResult : CommandResult
    {
        public SuccessCommandResult(
            object? data = null,
            string? message = null,
            HttpStatusCode httpStatusCode = HttpStatusCode.OK)
            : base(httpStatusCode, true, message, data)
        { }
    }
    public class NoContentCommandResult : CommandResult
    {
        public NoContentCommandResult(
            object? data = null,
            string? message = null)
            : base(HttpStatusCode.NoContent, false, message, data)
        { }
    }
    public class BadRequestCommandResult : CommandResult
    {
        public BadRequestCommandResult(
            object? data = null,
            string? message = null)
            : base(HttpStatusCode.BadRequest, false, message, data)
        { }
    }
    public class NotFoundCommandResult : CommandResult
    {
        public NotFoundCommandResult(
            object? data = null,
            string? message = null)
            : base(HttpStatusCode.NotFound, false, message, data)
        { }
    }
    public class UnauthorizedCommandResult : CommandResult
    {
        public UnauthorizedCommandResult(
            object? data = null,
            string? message = null)
            : base(HttpStatusCode.Unauthorized, false, message, data)
        { }
    }
    public class ErrorCommandResult : CommandResult
    {
        public ErrorCommandResult(
            object? data = null,
            string? message = null,
            string? exceptionMessage = null,
            string? innerExceptionMessage = null)
            : base(HttpStatusCode.InternalServerError, false, message, data, exceptionMessage, innerExceptionMessage)
        { }
    }

    public class ExceptionCommandResult : CommandResult
    {
        public ExceptionCommandResult(Exception ex)
            : base(HttpStatusCode.InternalServerError,
                false,
                "Houve uma falha inesperada.<br /><br />Caso persista, procure o Serviço de Atendimento ao Cliente.",
                null,
                ex.Message,
                ex.InnerException?.Message)
        {
            System.Diagnostics.Trace.Write(ex);
        }
    }
}
