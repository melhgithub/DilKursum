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
    public class EFEgitmenRepository : GenericRepository<Egitmen>, EgitmenDataAccess
    {
        public async Task<bool> CheckEgitmenExists(string kullaniciAdi, string email)
        {
            using (var context = new Context())
            {
                return await context.Set<Egitmen>()
                    .AnyAsync(e => e.KullaniciAdi == kullaniciAdi || e.Email == email);
            }
        }

        public async Task<Egitmen> GetByUserName(string egitmenUserName)
        {
            using (var context = new Context())
            {
                return await context.Egitmen.FirstOrDefaultAsync(c => c.KullaniciAdi == egitmenUserName);
            }
        }

        public async Task<List<string>> GetEgitmenUserNames()
        {

            using (var context = new Context())
            {
                return await context.Egitmen.Select(x => x.KullaniciAdi).ToListAsync();
            }
        }
    }
}
