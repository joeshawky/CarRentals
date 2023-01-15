using Core.Entities;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public class EntityRepositoryBase<TEntity, TContext>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()

    {

        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntry = context.Entry(entity);
                addedEntry.State = EntityState.Added;
                context.SaveChanges();
            }
        }
        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntry = context.Entry(entity);
                addedEntry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntry = context.Entry(entity);
                addedEntry.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity? Get(Func<TEntity, bool> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Func<TEntity, bool>? filter = null)
        {
            using (var context = new TContext())
            {
                var cars = filter is null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();

                return cars;
            }
        }



    }
}
