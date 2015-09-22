using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.IDAL
{
    public interface IRepository<T>
    {
        T Add(T entity);

        int Count(Expression<Func<T,bool>> precidate);

        bool Update(T entity);

        bool Delete(T entity);

        bool Exists(Expression<Func<T, bool>> anyLambda);

        T Find(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> Find<S>(Expression<Func<T, bool>> whereLambda, string strOrderName,bool bAsc);

        IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, string strOrderName, bool isAsc);
    }
}
