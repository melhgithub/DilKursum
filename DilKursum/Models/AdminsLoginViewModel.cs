using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class AdminsLoginViewModel
    {
        public List<Admin> Admins { get; set; }
        public AdminLoginDto LoginDto { get; set; }
    }
}
