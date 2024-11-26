using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ImageDataAccess : GenericDataAccess<Image>
    {
        Task<Image> GetImageByName(string name);
    }
}
