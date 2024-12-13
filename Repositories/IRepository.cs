using System.Linq.Expressions;

namespace OriginsOfDestiny.Repositories
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> Get(Expression<Func<T, bool>> expression);
    }
}
