using MediatR;

namespace ZupTeste.DataContracts.Queries
{
    public record ByIdQuery<TResult>(Guid Id) : IRequest<TResult>;
}