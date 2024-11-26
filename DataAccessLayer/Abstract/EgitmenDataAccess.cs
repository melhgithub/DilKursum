using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessLayer.Abstract
{
    public interface EgitmenDataAccess : GenericDataAccess<Egitmen>
    {
        Task<List<string>> GetEgitmenUserNames();

        Task<Egitmen> GetByUserName(string egitmenUserName);

        Task<bool> CheckEgitmenExists(string kullaniciAdi, string email);
    }
}
