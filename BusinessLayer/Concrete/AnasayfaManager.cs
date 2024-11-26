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
    public class AnasayfaManager : AnasayfaService
    {
        AnasayfaDataAccess _anasayfa;
        public AnasayfaManager(AnasayfaDataAccess anasayfa)
        {
            _anasayfa = anasayfa;
        }
        public async Task Add(Anasayfa entity)
        {
            await _anasayfa.Insert(entity);
        }

        public async Task Delete(Anasayfa entity)
        {
            await _anasayfa.Delete(entity);
        }

        public async Task<Anasayfa> GetByID(int ID)
        {
            return await _anasayfa.GetByID(ID);
        }

        public async Task<List<Anasayfa>> GetList()
        {
            return await _anasayfa.GetList();
        }

        public async Task Update(Anasayfa entity)
        {
            await _anasayfa.Update(entity);
        }
    }
}
