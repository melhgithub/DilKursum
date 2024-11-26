using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface KursDetailDataAccess : GenericDataAccess<KursDetail>
    {
        Task<List<KursDetail>> GetListWithIncludesAsync();
        Task<List<KursDetail>> GetListByKursID(int kursID);
        Task<KursDetail> GetByKursID(int kursID);
    }
}
