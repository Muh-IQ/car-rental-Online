using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGeneralTransactionRelationRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllByTransaction(int pageIndex, int pageSize, Expression<Func<T, bool>> expression, params string[] includes);
    }
  
}
