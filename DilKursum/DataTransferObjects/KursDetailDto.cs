using EntityLayer.Concrete;

namespace DilKursum.DataTransferObjects
{
    public class KursDetailDto
    {
        public int ID { get; set; }
        public string? VideoLink { get; set; }
        public string? ImageLink { get; set; }
        public string? Description { get; set; }
        public KursDetailDurum Durum { get; set; }
        public string KursAdi { get; set; }
        public string EgitmenKullaniciAdi { get; set; }
        public string DilAdi { get; set; }
        public List<string> Diller { get; set; }

        public List<string> Kurslar { get; set; }

        public List<string> Egitmenler { get; set; }

        //Bağlantılar
        public int KursID { get; set; }
        public Kurs Kurs { get; set; }
    }
}
