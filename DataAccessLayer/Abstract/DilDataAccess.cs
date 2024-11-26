using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface DilDataAccess : GenericDataAccess<Dil>
    {
        Task<List<string>> GetDilNames();
        Task<Dil> GetByDilAdi(string dilAdi);
    }
}
