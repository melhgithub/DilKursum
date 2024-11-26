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
    public class LinkManager : LinkService
    {
        LinkDataAccess _link;
        public LinkManager(LinkDataAccess link)
        {
            _link = link;
        }
        public async Task Add(Link entity)
        {
            await _link.Insert(entity);
        }

        public async Task Delete(Link entity)
        {
            await _link.Delete(entity);
        }

        public async Task<Link> GetByID(int ID)
        {
            return await _link.GetByID(ID);
        }

        public async Task<List<Link>> GetList()
        {
            return await _link.GetList();
        }

        public async Task Update(Link entity)
        {
            await _link.Update(entity);
        }
    }
}
