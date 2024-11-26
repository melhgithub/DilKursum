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
    public class IletisimManager : IletisimService
    {
        IletisimDataAccess _iletisim;
        public IletisimManager(IletisimDataAccess Iletisim)
        {
            _iletisim = Iletisim;
        }
        public async Task Add(Iletisim entity)
        {
            await _iletisim.Insert(entity);
        }

        public async Task Delete(Iletisim entity)
        {
            await _iletisim.Delete(entity);
        }

        public async Task<Iletisim> GetByID(int ID)
        {
            return await _iletisim.GetByID(ID);
        }

        public async Task<List<Iletisim>> GetList()
        {
            return await _iletisim.GetList();
        }

        public async Task Update(Iletisim entity)
        {
            await _iletisim.Update(entity);
        }
    }
}
