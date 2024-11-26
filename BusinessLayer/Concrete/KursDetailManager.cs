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
    public class KursDetailManager : KursDetailService
    {
        KursDetailDataAccess _kursDetail;
        public KursDetailManager(KursDetailDataAccess kursDetail)
        {
            _kursDetail = kursDetail;
        }
        public async Task Add(KursDetail entity)
        {
            await _kursDetail.Insert(entity);
        }

        public async Task Delete(KursDetail entity)
        {
            await _kursDetail.Delete(entity);
        }

        public async Task<KursDetail> GetByID(int ID)
        {
            return await _kursDetail.GetByID(ID);
        }

        public async Task<KursDetail> GetByKursID(int kursID)
        {
            return await _kursDetail.GetByKursID(kursID);
        }

        public async Task<List<KursDetail>> GetList()
        {
            return await _kursDetail.GetList();
        }

        public async Task<List<KursDetail>> GetListByKursID(int kursID)
        {
            return await _kursDetail.GetListByKursID(kursID);
        }

        public async Task<List<KursDetail>> GetListWithIncludesAsync()
        {
            return await _kursDetail.GetListWithIncludesAsync();
        }

        public async Task Update(KursDetail entity)
        {
            await _kursDetail.Update(entity);
        }
    }
}
