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
    public class EFKursFileRepository : GenericRepository<KursFile>, KursFileDataAccess
    {
        public async Task<KursFile> GetImageByName(string name)
        {
            using (var context = new Context())
            {
                return await context.KursFile.FirstOrDefaultAsync(c => c.Name == name);
            }
        }
        public async Task<KursFile> GetVideoByName(string name)
        {
            using (var context = new Context())
            {
                return await context.KursFile.FirstOrDefaultAsync(c => c.Name == name);
            }
        }
        public async Task<KursFile> GetImageByFileName(string name)
        {
            using (var context = new Context())
            {
                return await context.KursFile.FirstOrDefaultAsync(c => c.FileName == name);
            }
        }
        public async Task<KursFile> GetVideoByFileName(string name)
        {
            using (var context = new Context())
            {
                return await context.KursFile.FirstOrDefaultAsync(c => c.FileName == name);
            }
        }
        public async Task<List<KursFile>> GetListWithIncludesAsync()
        {
            using (var context = new Context())
            {
                return await context.KursFile
                    .Include(p => p.Egitmen).
                    ToListAsync();
            }
        }
    }
}
