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
    public class FooterManager : FooterService
    {
        FooterDataAccess _footer;
        public FooterManager(FooterDataAccess Footer)
        {
            _footer = Footer;
        }
        public async Task Add(Footer entity)
        {
            await _footer.Insert(entity);
        }

        public async Task Delete(Footer entity)
        {
            await _footer.Delete(entity);
        }

        public async Task<Footer> GetByID(int ID)
        {
            return await _footer.GetByID(ID);
        }

        public async Task<List<Footer>> GetList()
        {
            return await _footer.GetList();
        }

        public async Task Update(Footer entity)
        {
            await _footer.Update(entity);
        }
    }
}
