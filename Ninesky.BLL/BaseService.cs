using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.BLL
{
    public class BaseService<T>:IService<T> where T :class
    {
        protected IRepository<T> CurrentRepository { get; set; }

        public BaseService(IRepository<T> currentRepository) { CurrentRepository = currentRepository; }

        public T Add(T entity) { return CurrentRepository.Add(entity); }

        public bool Update(T entity) { return CurrentRepository.Update(entity); }

        public bool Delete(T entity) { return CurrentRepository.Delete(entity); }

    }
}
