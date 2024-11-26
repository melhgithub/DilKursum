using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace DilKursum.DataTransferObjects
{
    public class KursiyerDto
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Sifre { get; set; }
        public string SifreTekrar { get; set; }
        public decimal Bakiye { get; set; }
        public KursiyerDurum Durum { get; set; }
        public string FotoğrafUrl { get; set; }
    }
}
