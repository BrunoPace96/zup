using MediatR;

namespace ZupTeste.DataContracts.Commands
{
    public record ByIdCommand<TResult>(Guid Id) : IRequest<TResult>;
}