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
    public class HakkimizdaManager : HakkimizdaService
    {
        HakkimizdaDataAccess _hakkimizda;
        public HakkimizdaManager(HakkimizdaDataAccess hakkimizda)
        {
            _hakkimizda = hakkimizda;
        }
        public async Task Add(Hakkimizda entity)
        {
            await _hakkimizda.Insert(entity);
        }

        public async Task Delete(Hakkimizda entity)
        {
            await _hakkimizda.Delete(entity);
        }

        public async Task<Hakkimizda> GetByID(int ID)
        {
            return await _hakkimizda.GetByID(ID);
        }

        public async Task<List<Hakkimizda>> GetList()
        {
            return await _hakkimizda.GetList();
        }

        public async Task Update(Hakkimizda entity)
        {
            await _hakkimizda.Update(entity);
        }
    }
}
