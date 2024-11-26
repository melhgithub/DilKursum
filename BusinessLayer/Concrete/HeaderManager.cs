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
    public class HeaderManager : HeaderService
    {
        HeaderDataAccess _header;
        public HeaderManager(HeaderDataAccess header)
        {
            _header = header;
        }
        public async Task Add(Header entity)
        {
            await _header.Insert(entity);
        }

        public async Task Delete(Header entity)
        {
            await _header.Delete(entity);
        }

        public async Task<Header> GetByID(int ID)
        {
            return await _header.GetByID(ID);
        }

        public async Task<List<Header>> GetList()
        {
            return await _header.GetList();
        }

        public async Task Update(Header entity)
        {
            await _header.Update(entity);
        }
    }
}
