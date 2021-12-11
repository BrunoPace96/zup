using MediatR;

namespace ZupTeste.DataContracts.Queries
{
    public record EmptyQuery<TResult> : IRequest<TResult>;
}