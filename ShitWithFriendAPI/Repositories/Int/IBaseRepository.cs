using ShitWithFriendAPI.Entities;
using System.Linq.Expressions;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IBaseRepository<T> : IQueryable<T> where T : IBaseEntity
    {
        public IQueryable<T> Exist(Guid id, string errorMessage);
        public T Add(T entity);
        public void Delete(T entity);
        public IQueryable<T> GetAll();
        public IQueryable<T> FindAll(Expression<Func<T, bool>> expression);
        public IQueryable<T> GetById(Guid id);
        public bool SaveChanges();

    }
}