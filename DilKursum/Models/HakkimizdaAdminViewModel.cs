using EntityLayer.Concrete;
using DilKursum.DataTransferObjects;

namespace DilKursum.Models
{
    public class HakkimizdaAdminViewModel
    {
        public HakkimizdaEditDto Dto { get; set; }
        public List<Hakkimizda> Hakkimizda { get; set; }
        public List<Image> Images { get; set; }
    }
}
