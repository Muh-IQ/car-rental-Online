
using AddChainServiceLibrary;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class GeneralRepositoryBusiness<T>(IGeneralRepository<T> _Model) : IGeneralRepository<T> where T : class, AddChainServiceLibrary.IEntity
    {
        public async Task<T> GetById(int id)
        {
            return await _Model.GetById(id);
        }


        public Task<int> Add(T t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAll()
        {
            throw new NotImplementedException();
        }



        public async Task<bool> Update(T t, int ID)
        {
            return await _Model.Update(t, ID);
        }

        public Task<int> CountElement(Expression<Func<T, bool>> expression)
        {
            return  _Model.CountElement(expression);
        }
    }
}
