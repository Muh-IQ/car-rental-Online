using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AddChainServiceLibrary;

namespace Repository
{
    public interface IGeneralRepository<T> where T : class, AddChainServiceLibrary.IEntity
    {
        Task<T> GetById(int id);
        Task<T> GetAll();
        Task<int> Add(T t);
        public Task<bool> Update(T t, int ID);
        Task<bool> Delete(int id);
        Task<int> CountElement(Expression<Func<T,bool>> expression);

    }
}
