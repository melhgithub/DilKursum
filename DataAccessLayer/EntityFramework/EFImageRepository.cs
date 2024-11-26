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
    public class EFImageRepository : GenericRepository<Image>, ImageDataAccess
    {
        public async Task<Image> GetImageByName(string name)
        {
            using (var context = new Context())
            {
                return await context.Image.FirstOrDefaultAsync(c => c.Name == name);
            }
        }
    }
}
