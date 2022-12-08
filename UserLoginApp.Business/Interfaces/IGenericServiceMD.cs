using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Entities.Interfaces;

namespace UserLoginApp.Business.Interfaces
{
    public interface IGenericServiceMD<TEntity>
        where TEntity :IEntity
    {
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> expression);
        void Update(TEntity entity);
        IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression);
    }
}
