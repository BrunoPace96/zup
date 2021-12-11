using MediatR;

namespace ZupTeste.DataContracts.Commands
{
    public record EmptyCommand<TResult> : IRequest<TResult>;
}