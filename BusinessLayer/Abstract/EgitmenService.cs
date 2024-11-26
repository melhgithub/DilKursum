using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface EgitmenService : GenericService<Egitmen>
    {
        Task<List<string>> GetEgitmenUserNames();
        Task<int> GetEgitmenIdByUserName(string userName);
        Task<bool> CheckEgitmenExists(string kullaniciAdi, string email);
    }
}
