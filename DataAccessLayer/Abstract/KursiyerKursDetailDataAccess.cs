using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface KursiyerKursDetailDataAccess : GenericDataAccess<KursiyerKursDetail>
    {
        Task<List<KursiyerKursDetail>> GetListWithIncludesAsync();
        Task<List<KursiyerKursDetail>> GetListByKursiyerID(int kursiyerID);
    }
}
