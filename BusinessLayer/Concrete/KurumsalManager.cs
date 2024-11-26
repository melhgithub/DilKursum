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
    public class KurumsalManager : KurumsalService
    {
        KurumsalDataAccess _kurumsal;
        public KurumsalManager(KurumsalDataAccess Kurumsal)
        {
            _kurumsal = Kurumsal;
        }
        public async Task Add(Kurumsal entity)
        {
            await _kurumsal.Insert(entity);
        }

        public async Task Delete(Kurumsal entity)
        {
            await _kurumsal.Delete(entity);
        }

        public async Task<Kurumsal> GetByID(int ID)
        {
            return await _kurumsal.GetByID(ID);
        }

        public async Task<List<Kurumsal>> GetList()
        {
            return await _kurumsal.GetList();
        }

        public async Task Update(Kurumsal entity)
        {
            await _kurumsal.Update(entity);
        }
    }
}
