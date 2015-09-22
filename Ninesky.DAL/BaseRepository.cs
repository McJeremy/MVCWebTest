using Ninesky.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Ninesky.DAL
{
    public class BaseRepository<T> : IRepository<T> where T :class
    {
        protected NineskyDbContext context = ContextFactory.GetCurrentContext();

        public T Add(T entity)
        {
            context.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;            
            context.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> precidate)
        {
            return context.Set<T>().Where(precidate).Count();
        }

        public bool Delete(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }

        public bool Exists(Expression<Func<T, bool>> anyLambda)
        {
            return context.Set<T>().Any(anyLambda);
            //return Count(anyLambda) > 0;
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = context.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;

        }

        public IQueryable<T> Find<S>(Expression<Func<T, bool>> whereLambda, string strOrderName,bool bAsc)
        {
            var list= context.Set<T>().Where(whereLambda);

            list = OrderBy(list, strOrderName, bAsc);
            //if (bAsc)
            //{
            //    list = list.OrderBy<T, S>(orderLambda);
            //}
            //else
            //{
            //    list=list.OrderByDescending<T,S>(orderLambda);
            //}
            return list;
        }

        public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLambda, string strOrderName, bool bAsc)
        {
            var list = context.Set<T>().Where(whereLambda);
            totalRecord = list.Count();

            list = OrderBy(list, strOrderName, bAsc);

            //if (bAsc)
            //{
            //    list = list.OrderBy<T, S>(orderLambda);
            //}
            //else
            //{
            //    list = list.OrderByDescending<T, S>(orderLambda);
            //}

            list = list.Skip<T>((pageIndex - 1) * pageSize).Take(pageSize);

            return list;
        }

        public bool Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;            
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">原IQueryable</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>排序后的IQueryable<T></returns>
        private IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null) throw new ArgumentNullException("source", "不能为空");
            if (string.IsNullOrEmpty(propertyName)) return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null) throw new ArgumentNullException("propertyName", "属性不存在");
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }
    }
}
