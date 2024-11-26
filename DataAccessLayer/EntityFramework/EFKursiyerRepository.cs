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
    public class EFKursiyerRepository : GenericRepository<Kursiyer>, KursiyerDataAccess
    {
        public async Task<bool> CheckKursiyerExists(string kullaniciAdi, string email)
        {
            using (var context = new Context())
            {
                return await context.Set<Kursiyer>()
                    .AnyAsync(e => e.KullaniciAdi == kullaniciAdi || e.Email == email);
            }
        }
        public async Task<Kursiyer> GetKursiyerIDByKursiyerName(string kursiyerName)
        {
            using (var context = new Context())
            {
                return await context.Kursiyer.FirstOrDefaultAsync(x => x.KullaniciAdi == kursiyerName);
            }
        }

        public async Task<List<string>> GetKursiyerUserNames()
        {
            using (var context = new Context())
            {
                return await context.Kursiyer.Select(x => x.KullaniciAdi).ToListAsync();
            }
        }
    }
}
