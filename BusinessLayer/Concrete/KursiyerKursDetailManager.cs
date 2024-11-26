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
    public class KursiyerKursDetailManager : KursiyerKursDetailService
    {
        KursiyerKursDetailDataAccess _kursiyerKursDetail;
        public KursiyerKursDetailManager(KursiyerKursDetailDataAccess kursiyerKursDetail)
        {
            _kursiyerKursDetail = kursiyerKursDetail;
        }
        public async Task Add(KursiyerKursDetail entity)
        {
            await _kursiyerKursDetail.Insert(entity);
        }

        public async Task Delete(KursiyerKursDetail entity)
        {
            await _kursiyerKursDetail.Delete(entity);
        }

        public async Task<KursiyerKursDetail> GetByID(int ID)
        {
            return await _kursiyerKursDetail.GetByID(ID);
        }

        public async Task<List<KursiyerKursDetail>> GetList()
        {
            return await _kursiyerKursDetail.GetList();
        }

        public async Task<List<KursiyerKursDetail>> GetListByKursiyerID(int kursiyerID)
        {
            return await _kursiyerKursDetail.GetListByKursiyerID(kursiyerID);
        }

        public async Task<List<KursiyerKursDetail>> GetListWithIncludesAsync()
        {
            return await _kursiyerKursDetail.GetListWithIncludesAsync();
        }

        public async Task Update(KursiyerKursDetail entity)
        {
            await _kursiyerKursDetail.Update(entity);
        }
    }
}
