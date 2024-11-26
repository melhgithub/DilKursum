using EntityLayer.Concrete;

namespace DilKursum.DataTransferObjects
{
    public class KursiyerKursDetailsAddDto
    {
        public int ID { get; set; }
        public KursiyerKursDetailDurum Durum { get; set; }

        public string KursAdi { get; set; }
        public int KursID { get; set; }
        public string KursiyerKullaniciAdi { get; set; }
    }
}
