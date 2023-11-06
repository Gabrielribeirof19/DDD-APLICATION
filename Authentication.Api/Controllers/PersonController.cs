using authentication.domain.Commands;
using authentication.domain.Handlers;
using authentication.Domain.Commands;
using authentication.Domain.Handlers;
using Authentication.Api.Extension;
using Microsoft.AspNetCore.Mvc;


namespace Authentication.Api.Controller
{
    [ApiController]
    [Route("/v1/person/")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;


        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }


        [HttpPost("create-person")]
        public async Task<object> CreatePerson
        (
            [FromBody] CreatePersonCommand command,
            [FromServices] PersonHandler handler
        )
        {
            try
            {
                Console.WriteLine("entrei na controller");
                return await handler.Handle(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating person");
                return BadRequest();
            }
        }
        [Route("authenticate")]
        [HttpPost]
        public async Task<object> Authenticate
        (
            [FromBody] AuthenticateCommand command,
            [FromServices] AuthenticateHandler handler
        )
        {
            try
            {
                var authentication = await handler.Handle(command);

                var token = JwtExtension.Generate(command);

                return Results.Ok(new
                {
                    authentication,
                    token
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error authenticating person");
                return BadRequest();
            }
        }
    }
}