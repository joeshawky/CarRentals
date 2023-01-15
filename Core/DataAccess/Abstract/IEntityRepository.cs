using Core.Entities;
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Func<T, bool>? filter = null);
        T? Get(Func<T, bool> filter);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
