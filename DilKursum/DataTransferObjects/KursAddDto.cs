using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace DilKursum.DataTransferObjects
{
    public class KursAddDto
    {
        public int ID { get; set; }
        public string KursAdi { get; set; }
        public string KursAciklama { get; set; }
        public int DersSayisi { get; set; }

        private string _fiyat;

        public string Fiyat
        {
            get { return _fiyat; }
            set { _fiyat = value.Replace(",", "."); }
        }
        public KursDurum Durum { get; set; }

        //Bağlantılar

        public string EgitmenKullaniciAdi { get; set; }

        public string DilAdi { get; set; }
    }
}
