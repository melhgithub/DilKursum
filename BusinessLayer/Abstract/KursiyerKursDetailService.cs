using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface KursiyerKursDetailService : GenericService<KursiyerKursDetail>
    {
        Task<List<KursiyerKursDetail>> GetListWithIncludesAsync();
        Task<List<KursiyerKursDetail>> GetListByKursiyerID(int kursiyerID);
    }
}
