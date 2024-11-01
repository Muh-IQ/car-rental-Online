using Data.Module;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class GeneralTransactionRelationRepository_Data<T>(AppDbContext _context) : IGeneralTransactionRelationRepository<T> where T : class
    {

        public  async Task<IEnumerable<T>> GetAllByTransaction(int pageIndex, int pageSize, Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression!=null)
            {
                query = query.Where(expression);
            }


            foreach (var include in includes)
                query = query.Include(include);

            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                
                .ToListAsync();
        }

    }
}
