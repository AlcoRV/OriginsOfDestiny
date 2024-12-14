using System.Linq.Expressions;

namespace OriginsOfDestiny.Repositories
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> Get(Expression<Func<T, bool>> expression);

        public void Create(T entity);

        public void Update(T entity);

        public void Delete(T entity);
    }
}
