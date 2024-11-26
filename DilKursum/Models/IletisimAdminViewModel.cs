using EntityLayer.Concrete;
using DilKursum.DataTransferObjects;

namespace DilKursum.Models
{
    public class IletisimAdminViewModel
    {
        public IletisimEditDto Dto { get; set; }
        public List<Iletisim> Iletisim { get; set; }
        public List<Image> Images { get; set; }
    }
}
