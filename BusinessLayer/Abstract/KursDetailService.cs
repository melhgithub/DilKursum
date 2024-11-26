using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface KursDetailService : GenericService<KursDetail>
    {
        Task<List<KursDetail>> GetListWithIncludesAsync();
        Task<KursDetail> GetByKursID(int kursID);
        Task<List<KursDetail>> GetListByKursID(int kursID);
    }
}
