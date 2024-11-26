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
    public class EgitmenManager : EgitmenService
    {
        EgitmenDataAccess _egitmen;
        public EgitmenManager(EgitmenDataAccess egitmen)
        {
            _egitmen = egitmen;
        }
        public async Task Add(Egitmen entity)
        {
            await _egitmen.Insert(entity);
        }

        public async Task<bool> CheckEgitmenExists(string kullaniciAdi, string email)
        {
            return await _egitmen.CheckEgitmenExists(kullaniciAdi, email);
        }

        public async Task Delete(Egitmen entity)
        {
            await _egitmen.Delete(entity);
        }

        public async Task<Egitmen> GetByID(int ID)
        {
            return await _egitmen.GetByID(ID);
        }


        public async Task<int> GetEgitmenIdByUserName(string userName)
        {
            var egitmen = await _egitmen.GetByUserName(userName);
            return egitmen != null ? egitmen.ID : 0;
        }

        public async Task<List<string>> GetEgitmenUserNames()
        {
            return await _egitmen.GetEgitmenUserNames();
        }

        public async Task<List<Egitmen>> GetList()
        {
            return await _egitmen.GetList();
        }

        public async Task Update(Egitmen entity)
        {
            await _egitmen.Update(entity);
        }
    }
}
