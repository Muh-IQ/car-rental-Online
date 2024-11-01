using AddChainServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ImplementaionServices
{
    public class ImplementaionAddChainService<T>(AppDbContext context) : IAddChainService<T> where T : class,IEntity
    {
        public async Task<int> AddToDb(T t)
        {
            await context.Set<T>().AddAsync(t);
            await context.SaveChangesAsync();
            return t._ID;
        }
    }
}
