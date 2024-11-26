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
    public class EFKursDetailRepository : GenericRepository<KursDetail>, KursDetailDataAccess
    {

        public async Task<KursDetail> GetByKursID(int kursID)
        {
            using (var context = new Context()) { 
                return await context.KursDetail.FirstOrDefaultAsync(x=>x.KursID==kursID);            
            }
        }

        public async Task<List<KursDetail>> GetListByKursID(int kursID)
        {

            using (var context = new Context())
            {
                var kursDetails = await context.KursDetail
                    .Where(kd => kd.KursID == kursID)
                    .Include(kd => kd.Kurs)
                    .ThenInclude(k => k.Dil)
                    .ToListAsync();

                return kursDetails;
            }
        }

        public async Task<List<KursDetail>> GetListWithIncludesAsync()
        {
            using (var context = new Context())
            {
                return await context.KursDetail
            .Include(kd => kd.Kurs)
            .ThenInclude(k => k.Dil) 
            .Include(kd => kd.Kurs.Egitmen)
            .ToListAsync();
            }
        }
    }
}
