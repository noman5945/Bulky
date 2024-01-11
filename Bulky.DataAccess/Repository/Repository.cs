using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> dbSet;
        //Include shows the foregin key props
        public Repository(ApplicationDBContext db) { 
            _db = db;
            this.dbSet=_db.Set<T>();
            _db.Products.Include(u=>u.Category).Include(u=>u.CategoryId);
        }
        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null) 
            {
                foreach (var property in includeProperties.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query=query.Include(property);
                }
            }
            return query.ToList();
        }

        public void Remove(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public void RemoveRange(IEnumerable<T> Entities)
        {
            dbSet.RemoveRange(Entities);
        }
    }
}
