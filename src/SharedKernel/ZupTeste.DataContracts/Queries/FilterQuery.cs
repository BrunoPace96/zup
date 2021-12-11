using MediatR;

namespace ZupTeste.DataContracts.Queries
{
    public record FilterQuery<TResult>(
        int Page = 1,
        int PageSize = 10,
        string Filter = ""
    ) : IRequest<TResult>;
}