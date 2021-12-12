using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZupTeste.API.Common.Controllers;
using ZupTeste.DataContracts.Commands;
using ZupTeste.DataContracts.Queries;
using ZupTeste.DataContracts.Results;
using ZupTeste.Domain.Funcionarios.Read;
using ZupTeste.Domain.Funcionarios.Read.ObterFuncionarioPeloId;
using ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios;
using ZupTeste.Domain.Funcionarios.Write;
using ZupTeste.Domain.Funcionarios.Write.AtualizarFuncionario;
using ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;
using ZupTeste.Domain.Funcionarios.Write.DeletarFuncionario;

namespace ZupTeste.API.Controllers;

[Route("api/[controller]")]
public class FuncionariosController : BaseController
{
    private readonly IMediator _mediator;

    public FuncionariosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CriarFuncionarioResult>> Post(
        [FromBody] CriarFuncionarioCommand command,
        CancellationToken cancellationToken = new ())
    {
        var result = await _mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PaginatedResult<ObterListaFuncionariosResult>>> List(
        [FromQuery] PaginatedQuery<PaginatedResult<ObterListaFuncionariosResult>> command,
        CancellationToken cancellationToken = new ())
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PaginatedResult<ObterListaFuncionariosResult>>> Get(
        [FromRoute] ByIdQuery<ObterFuncionarioPeloIdResult> command,
        CancellationToken cancellationToken = new ())
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AtualizarFuncionarioResult>> Put(
        [FromBody] AtualizarFuncionarioCommand command,
        CancellationToken cancellationToken = new ())
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromRoute] ByIdCommand<DeletarFuncionarioResult> command,
        CancellationToken cancellationToken = new ())
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if(result is not { Sucesso: true })
            return BadRequest();

        return NoContent();
    }
}