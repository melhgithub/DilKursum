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
    public class KursiyerManager : KursiyerService
    {
        KursiyerDataAccess _kursiyer;
        public KursiyerManager(KursiyerDataAccess kursiyer)
        {
            _kursiyer = kursiyer;
        }
        public async Task Add(Kursiyer entity)
        {
            await _kursiyer.Insert(entity);
        }

        public async Task<bool> CheckKursiyerExists(string kullaniciAdi, string email)
        {
            return await _kursiyer.CheckKursiyerExists(kullaniciAdi,email);
        }

        public async Task Delete(Kursiyer entity)
        {
            await _kursiyer.Delete(entity);
        }

        public async Task<Kursiyer> GetByID(int ID)
        {
            return await _kursiyer.GetByID(ID);
        }

        public async Task<int> GetKursiyerIDByKursiyerName(string kursiyerName)
        {
            var kursiyer = await _kursiyer.GetKursiyerIDByKursiyerName(kursiyerName);
            return kursiyer != null ? kursiyer.ID : 0;
        }

        public async Task<List<string>> GetKursiyerUserNames()
        {
            return await _kursiyer.GetKursiyerUserNames();
        }

        public async Task<List<Kursiyer>> GetList()
        {
            return await _kursiyer.GetList();
        }

        public async Task Update(Kursiyer entity)
        {
            await _kursiyer.Update(entity);
        }
    }
}
