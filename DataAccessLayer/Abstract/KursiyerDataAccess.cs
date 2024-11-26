using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface KursiyerDataAccess : GenericDataAccess<Kursiyer>
    {
        Task<List<string>> GetKursiyerUserNames();
        Task<Kursiyer> GetKursiyerIDByKursiyerName(string kursiyerName);
        Task<bool> CheckKursiyerExists(string kullaniciAdi, string email);
    }
}
