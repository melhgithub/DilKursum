using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class KurslarViewModel
    {
        public KursFilterDto DTO { get; set; }
        public List<Kurs> Kurslar { get; set; }
        public List<Dil> Diller { get; set; }
        public List<KursDetail> KursDetaylari { get; set; }
        public List<Egitmen> Egitmenler { get; set; }
        public List<KursFile> Dosyalar { get; set; }
        public Dictionary<int, int> KursiyerSayilari { get; set; }
    }
}
