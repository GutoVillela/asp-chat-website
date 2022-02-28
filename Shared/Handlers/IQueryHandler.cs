using Shared.Queries;

namespace Shared.Handlers
{
    public interface IQueryHandler<TRequest, TResult> where TRequest : IQueryRequest where TResult : IQueryResult
    {
        Task<TResult> HandleAsync(TRequest command);
    }
}
