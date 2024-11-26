using EntityLayer.Concrete;
using MADEO_HFT.DataTransferObjects;

namespace DilKursum.Models
{
    public class BannerAdminViewModel
    {
        public BannerEditDto Dto { get; set; }
        public List<Banner> Banner { get; set; }
        public List<Image> Images { get; set; }
    }
}
