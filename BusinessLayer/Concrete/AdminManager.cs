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
    public class AdminManager : AdminService
    {
        AdminDataAccess _admin;
        public AdminManager(AdminDataAccess admin)
        {
            _admin = admin;
        }
        public async Task Add(Admin entity)
        {
            await _admin.Insert(entity);
        }

        public async Task Delete(Admin entity)
        {
            await _admin.Delete(entity);
        }

        public async Task<Admin> GetByID(int ID)
        {
            return await _admin.GetByID(ID);
        }

        public async Task<List<Admin>> GetList()
        {
            return await _admin.GetList();
        }

        public async Task Update(Admin entity)
        {
            await _admin.Update(entity);
        }
    }
}
