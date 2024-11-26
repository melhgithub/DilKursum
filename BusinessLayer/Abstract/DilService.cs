using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface DilService : GenericService<Dil>
    {
        Task<List<string>> GetDilNames();

        Task<int> GetDilIdByDilAdi(string dilAdi);
    }
}
