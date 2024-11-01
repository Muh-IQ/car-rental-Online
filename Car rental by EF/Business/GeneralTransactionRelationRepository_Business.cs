using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class GeneralTransactionRelationRepository_Business<T>(IGeneralTransactionRelationRepository<T> _Model) : IGeneralTransactionRelationRepository<T> where T : class
    {
        public async Task<IEnumerable<T>> GetAllByTransaction(int pageIndex, int pageSize, Expression<Func<T, bool>> expression, params string[] includes)
        {
            return await _Model.GetAllByTransaction(pageIndex, pageSize, expression, includes);
        }
    }
}
