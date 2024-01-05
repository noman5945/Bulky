using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    /*This is a generic repository*/
    public interface IRepository<T> where T : class
    {
        //T-Category or any class model
        T Get(Expression<Func<T,bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T Entity);
        void Remove(T Entity);
        void RemoveRange(IEnumerable<T> Entities);
    }
}
