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
    public class EFKursRepository : GenericRepository<Kurs>, KursDataAccess
    {
        public async Task<Kurs> GetByKursName(string kursName)
        {
            using (var context = new Context())
            {
                return await context.Kurs.FirstOrDefaultAsync(x => x.KursAdi == kursName);
            }
        }

        public async Task<List<string>> GetKursNames()
        {
            using (var context = new Context())
            {
                return await context.Kurs.Select(x => x.KursAdi).ToListAsync();
            }
        }

        public async Task<List<Kurs>> GetListWithIncludesAsync()
        {
            using (var context = new Context())
            {
                return await context.Kurs
                    .Include(p => p.Egitmen)
                    .Include(p => p.Dil).
                    ToListAsync();
            }
        }
    }
}
