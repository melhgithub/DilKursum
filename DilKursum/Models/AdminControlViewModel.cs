using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class AdminControlViewModel
    {
        public List<Admin> Admins { get; set; }
        public AdminEditDto Dto { get; set; }
    }
}
