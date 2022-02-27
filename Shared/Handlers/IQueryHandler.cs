using Shared.Queries;

namespace Shared.Handlers
{
    public interface IQueryHandler<T> where T : IQueryRequest
    {
        Task<IQueryResult> HandleAsync(T command);
    }
}
