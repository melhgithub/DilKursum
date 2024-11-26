using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace DilKursum.DataTransferObjects
{
    public class KursDetailAddDto
    {
        public int ID { get; set; }
        public string? VideoLink { get; set; }
        public string? ImageLink { get; set; }
        public string? Description { get; set; }
        public KursDetailDurum Durum { get; set; }

        //Bağlantılar

        public string KursAdi { get; set; }
        public int KursID { get; set; }
        public Kurs Kurs { get; set; }
    }
}
