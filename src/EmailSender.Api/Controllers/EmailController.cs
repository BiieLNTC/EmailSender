using EmailSender.Application.Dtos;
using EmailSender.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmailController(SendEmailUseCase sendEmailUseCase) : ControllerBase
    {
        /// <summary>Dispara um e-mail.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Send(
            [FromBody] SendEmailRequest request,
            CancellationToken cancellationToken)
        {
            await sendEmailUseCase.ExecuteAsync(request, cancellationToken);
            return Ok();
        }
    }
}
