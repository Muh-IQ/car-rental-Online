using AddChainServiceLibrary;
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
    public class GeneralRepository_Data<T>(AppDbContext _context) :
        IGeneralRepository<T> where T : class, AddChainServiceLibrary.IEntity
    {
        public async Task<int> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return  entity._ID;
        }
        public async Task<int> CountElement(Expression<Func<T, bool>> expression)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);


            return query.ToList().Count();
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                 _context.Set<T>().Remove(item);
                return true;
            }
            return false;
        }
        public Task<T> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<bool> Update(T t ,int ID)
        {
            var Res = await _context.Set<T>().FindAsync(ID);

            if (Res == null) return false;

            Res = t;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
