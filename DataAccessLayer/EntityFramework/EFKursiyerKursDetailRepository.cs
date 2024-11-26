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
    public class EFKursiyerKursDetailRepository : GenericRepository<KursiyerKursDetail>, KursiyerKursDetailDataAccess
    {
        public async Task<List<KursiyerKursDetail>> GetListByKursiyerID(int kursiyerID)
        {
            using (var context = new Context())
            {
                var kursiyerKursDetails = await context.KursiyerKursDetail
                    .Where(kd => kd.KursiyerID == kursiyerID)
                    .Include(kd => kd.Kurs)
                    .ToListAsync();

                return kursiyerKursDetails;
            }
        }

        public async Task<List<KursiyerKursDetail>> GetListWithIncludesAsync()
        {

            using (var context = new Context())
            {
                return await context.KursiyerKursDetail
                    .Include(p => p.Kurs)
                    .Include(p => p.Kursiyer)
                    .Include(p => p.Kurs.Egitmen)
                    .Include(p => p.Kurs.Dil)
                    .ToListAsync();
            }
        }
    }
}
