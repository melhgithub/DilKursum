using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BannerManager : BannerService
    {
        BannerDataAccess _banner;
        public BannerManager(BannerDataAccess banner)
        {
            _banner = banner;
        }
        public async Task Add(Banner entity)
        {
            await _banner.Insert(entity);
        }

        public async Task Delete(Banner entity)
        {
            await _banner.Delete(entity);
        }

        public async Task<Banner> GetByID(int ID)
        {
            return await _banner.GetByID(ID);
        }

        public async Task<List<Banner>> GetList()
        {
            return await _banner.GetList();
        }

        public async Task Update(Banner entity)
        {
            await _banner.Update(entity);
        }
    }
}
