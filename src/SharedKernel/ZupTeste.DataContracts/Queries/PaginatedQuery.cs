using MediatR;

namespace ZupTeste.DataContracts.Queries
{
    public record PaginatedQuery<TResult>(
        int Page = 1,
        int PageSize = 10
    ) : IRequest<TResult>;
}