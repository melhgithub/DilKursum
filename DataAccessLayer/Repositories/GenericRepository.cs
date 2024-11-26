using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : GenericDataAccess<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        protected Context c = new Context();

        public async Task Insert(T t)
        {
            c.Add(t);
            await c.SaveChangesAsync();
        }
        public async Task Delete(T t)
        {
            c.Remove(t);
            await c.SaveChangesAsync();
        }

        public async Task Update(T t)
        {
            c.Update(t);
            await c.SaveChangesAsync();
        }

        public async Task<T> GetByID(int ID)
        {
            return await c.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "ID") == ID);
        }


        public async Task<List<T>> GetList()
        {
            return await c.Set<T>().ToListAsync();
        }
    }
}
