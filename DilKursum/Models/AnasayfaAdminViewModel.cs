using EntityLayer.Concrete;
using DilKursum.DataTransferObjects;

namespace DilKursum.Models
{
    public class AnasayfaAdminViewModel
    {
        public AnasayfaEditDto Dto { get; set; }
        public List<Anasayfa> Anasayfa { get; set; }
        public List<Image> Images { get; set; }
    }
}
