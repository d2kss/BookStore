using System.Linq.Expressions;

namespace BookStoreMgmt.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);

        void RemoveRange(IEnumerable<T> entity);

        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}
