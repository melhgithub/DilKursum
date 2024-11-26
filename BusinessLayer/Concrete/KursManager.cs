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
    public class KursManager : KursService
    {
        KursDataAccess _kurs;
        public KursManager(KursDataAccess kurs)
        {
            _kurs = kurs;   
        }
        public async Task Add(Kurs entity)
        {
            await _kurs.Insert(entity);
        }

        public async Task Delete(Kurs entity)
        {
            await _kurs.Delete(entity);
        }

        public async Task<Kurs> GetByID(int ID)
        {
            return await _kurs.GetByID(ID);
        }

        public async Task<int> GetKursIdByKursName(string kursName)
        {
            var kurs = await _kurs.GetByKursName(kursName);
            return kurs != null ? kurs.ID : 0;
        }

        public async Task<List<string>> GetKursNames()
        {
            return await _kurs.GetKursNames();
        }

        public async Task<List<Kurs>> GetList()
        {
            return await _kurs.GetList();
        }

        public async Task<List<Kurs>> GetListWithIncludesAsync()
        {
            return await _kurs.GetListWithIncludesAsync();
        }

        public async Task Update(Kurs entity)
        {
            await _kurs.Update(entity);
        }
    }
}
