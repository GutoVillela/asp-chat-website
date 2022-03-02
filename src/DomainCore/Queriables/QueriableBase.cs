using DomainCore.Entities;
using System.Linq.Expressions;

namespace DomainCore.Queriables
{
    public class QueriableBase<TEntity> where TEntity : Entity
    {
        public static Expression<Func<TEntity, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
    }
}
