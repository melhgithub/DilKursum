using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFDilRepository : GenericRepository<Dil>, DilDataAccess
    {
        public async Task<Dil> GetByDilAdi(string dilAdi)
        {
            using (var context = new Context())
            {
                return await context.Dil.FirstOrDefaultAsync(c => c.DilAdi == dilAdi);
            }
        }

        public async Task<List<string>> GetDilNames()
        {
            using (var context = new Context())
            {
                return await context.Dil.Select(x => x.DilAdi).ToListAsync();
            }
        }
    }
}
