using EntityLayer.Concrete;
using DilKursum.DataTransferObjects;

namespace DilKursum.Models
{
    public class KurumsalAdminViewModel
    {
        public KurumsalEditDto Dto { get; set; }
        public List<Kurumsal> Kurumsal { get; set; }
        public List<Image> Images { get; set; }
    }
}
