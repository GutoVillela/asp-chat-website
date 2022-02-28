using Shared.Entities;
using System.Linq.Expressions;

namespace Shared.Queriables
{
    public class QueriableBase<TEntity> where TEntity : Entity
    {
        public static Expression<Func<TEntity, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
    }
}
