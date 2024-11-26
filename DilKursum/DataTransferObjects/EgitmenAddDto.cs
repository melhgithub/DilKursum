using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace DilKursum.DataTransferObjects
{
    public class EgitmenAddDto
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Sifre { get; set; }
        public string KisaBilgi { get; set; }
        public EgitmenDurum Durum { get; set; }

        private string _bakiye;

        public string Bakiye
        {
            get { return _bakiye; }
            set { _bakiye = value.Replace(",", "."); }
        }
    }
}
