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
    public class DilManager : DilService
    {
        DilDataAccess _dil;
        public DilManager(DilDataAccess dil)
        {
            _dil = dil;
        }
        public async Task Add(Dil entity)
        {
            await _dil.Insert(entity);
        }

        public async Task Delete(Dil entity)
        {
            await _dil.Delete(entity);
        }

        public async Task<Dil> GetByID(int ID)
        {
            return await _dil.GetByID(ID);
        }

        public async Task<int> GetDilIdByDilAdi(string dilAdi)
        {
            var dil = await _dil.GetByDilAdi(dilAdi);
            return dil != null ? dil.ID : 0;
        }

        public async Task<List<string>> GetDilNames()
        {
            return await _dil.GetDilNames();
        }

        public async Task<List<Dil>> GetList()
        {
            return await _dil.GetList();
        }

        public async Task Update(Dil entity)
        {
            await _dil.Update(entity);
        }
    }
}
