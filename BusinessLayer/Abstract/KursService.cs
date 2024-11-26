using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface KursService : GenericService<Kurs>
    {
        Task<List<Kurs>> GetListWithIncludesAsync();
        Task<int> GetKursIdByKursName(string kursName);

        Task<List<string>> GetKursNames();
    }
}
