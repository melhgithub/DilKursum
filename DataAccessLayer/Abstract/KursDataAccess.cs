using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface KursDataAccess : GenericDataAccess<Kurs>
    {
        Task<List<string>> GetKursNames();
        Task<List<Kurs>> GetListWithIncludesAsync();

        Task<Kurs> GetByKursName(string kursName);
    }
}
