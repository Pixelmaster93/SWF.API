using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.DBContext;
using System.Collections;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        public SWFContext _context { get; }
        private DbSet<T> _dbSet;

        public BaseRepository(SWFContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public DbSet<T> Set => this._dbSet ??= this._context.Set<T>();

        public IQueryable<T> Exist(Guid id, string errorMessage)
        {
            var entities = GetById(id);

            if (entities.Count() == 0)
                throw new Exception(errorMessage);

            var isDeleted = false;

            return entities;
        }
        public T Add(T entity)
        {
            var result = _context.Set<T>().Add(entity);
            return result.Entity;
        }

        public void Delete(T entity) =>
            _context.Remove(entity);

        public IQueryable<T> FindAll(Expression<Func<T, bool>> expression) =>
            _dbSet.AsQueryable().Where(expression);

        public IQueryable<T> GetAll() =>
            _dbSet;

        public IQueryable<T> GetById(Guid id) =>
             _dbSet.Where(x => x.Id == id);

        public bool SaveChanges() =>
             _context.SaveChanges() >= 0;

        #region Implementation of Enumerable and IQuerable
        public IEnumerator<T> GetEnumerator()
        {
            return this.Set.AsQueryable().AsEnumerable().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Type ElementType
        {
            get { return (this.Set.AsQueryable() as IQueryable).ElementType; }
        }

        public Expression Expression
        {
            get { return (this.Set.AsQueryable() as IQueryable).Expression; }
        }

        public IQueryProvider Provider
        {
            get { return (this.Set.AsQueryable() as IQueryable).Provider; }
        }
        #endregion
    }
}
