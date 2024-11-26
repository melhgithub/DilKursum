using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface KursiyerService : GenericService<Kursiyer>
    {
        Task<List<string>> GetKursiyerUserNames();
        Task<int> GetKursiyerIDByKursiyerName(string kursiyerName);
        Task<bool> CheckKursiyerExists(string kullaniciAdi, string email);
    }
}
