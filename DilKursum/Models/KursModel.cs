using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class KursModel
    {
        public KursDto DTO { get; set; }
        public List<Dil> Diller { get; set; }
        public List<Egitmen> Egitmenler { get; set; }
        public List<Kurs> Kurslar { get; set; }
        public List<KursDetail> KursDetaylari { get; set; }

    }
}
