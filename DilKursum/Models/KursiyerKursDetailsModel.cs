using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class KursiyerKursDetailsModel
    {
        public KursiyerKursDetailsDto DTO { get; set; }
        public List<Dil> Diller { get; set; }
        public List<Egitmen> Egitmenler { get; set; }
        public List<Kurs> Kurslar { get; set; }
        public List<KursDetail> KursDetaylari { get; set; }
        public List<Kursiyer> Kursiyerler { get; set; }
        public List<KursiyerKursDetail> KursiyerKursDetaylari { get; set; }

    }
}
