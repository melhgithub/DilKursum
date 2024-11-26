using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface KursFileDataAccess : GenericDataAccess<KursFile>
    {
        Task<KursFile> GetImageByName(string name);
        Task<KursFile> GetImageByFileName(string name);
        Task<KursFile> GetVideoByName(string name);
        Task<KursFile> GetVideoByFileName(string name);
        Task<List<KursFile>> GetListWithIncludesAsync();
    }
}
