using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Business.Interfaces;
using UserLoginApp.DataAccess.Interfaces;
using UserLoginApp.Entities.Interfaces;

namespace UserLoginApp.Business.Concrete
{
    public class GenericManagerMD<TEntity> : IGenericServiceMD<TEntity> where TEntity :IEntity
    {
        private readonly IGenericRepositoryMD<TEntity> _genericRepositoryMD;

        public GenericManagerMD(IGenericRepositoryMD<TEntity> genericRepositoryMD)
        {
            _genericRepositoryMD=genericRepositoryMD;
        }

        public void Add(TEntity entity)
        {
            _genericRepositoryMD.Add(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _genericRepositoryMD.Get(expression);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _genericRepositoryMD.GetAll();
        }

        public IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression)
        {
            return _genericRepositoryMD.GetAllByFilter(expression);
        }

        public void Remove(Expression<Func<TEntity, bool>> expression)
        {
             _genericRepositoryMD.Remove(expression);
        }

        public void Update(TEntity entity)
        {
            _genericRepositoryMD.Update(entity);
        }
    }
}
