using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZupTeste.API.Authentication;
using ZupTeste.API.Authentication.Contracts;
using ZupTeste.API.Common.Controllers;
using ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha;

namespace ZupTeste.API.Controllers;

[Route("api/[controller]")]
public class AdministradoresController : BaseController
{
    private readonly IMediator _mediator;
    private readonly JwtHelper _jwtHelper;

    public AdministradoresController(
        IMediator mediator,
        JwtHelper jwtHelper)
    {
        _mediator = mediator;
        _jwtHelper = jwtHelper;
    }

    [AllowAnonymous]
    [HttpPost("autenticar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<RespostaToken>> Post(
        [FromBody] ObterAdministradorPorEmailSenhaQuery command,
        CancellationToken cancellationToken = new())
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result != null)
        {
            var token = _jwtHelper.GenerateToken(new AdministradorAutenticado
            {
                Id = result.Id,
                Name = result.Nome,
                Email = result.Email
            });

            return Ok(token);
        }

        return null;
    }
}