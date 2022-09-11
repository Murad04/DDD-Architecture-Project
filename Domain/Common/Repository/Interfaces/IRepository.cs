using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Repository.Interfaces
{
    public interface IRepository<TEntity, in TPrimaryKey> :IDisposable where TEntity : BaseEntity<TPrimaryKey>   
    {
        IQueryable<TEntity> GetAll();
        
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAllList();

        Task<List<TEntity>> GetAllListIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        ValueTask<TEntity> Find(TPrimaryKey id);

        Task<TEntity> GetFirst(TPrimaryKey id);

        Task<TEntity> GetFirstIncluding(TPrimaryKey id,params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);

        //Task<TEntity> GetFirstIncluding(Expression<Func>)

    }
}
