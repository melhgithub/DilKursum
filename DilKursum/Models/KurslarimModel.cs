using EntityLayer.Concrete;
using System.Collections.Generic;

namespace DilKursum.Models
{
    public class KurslarimModel
    {
        public List<Kurs> Kurslar { get; set; }
        public List<Dil> Diller { get; set; }
        public List<KursDetail> KursDetaylari { get; set; }
        public List<Egitmen> Egitmenler { get; set; }
        public List<KursFile> Dosyalar { get; set; }
        public Dictionary<int, int> KursiyerSayilari { get; set; }
    }
}
