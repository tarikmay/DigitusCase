using System.Linq.Expressions;
using UserLoginApp.Entities.Interfaces;

namespace UserLoginApp.DataAccess.Interfaces
{
    public interface IGenericRepositoryMD<TEntity> 
        where TEntity : IEntity
    {
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> expression);
        void Update(TEntity entity);
        IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression);
    }
}
