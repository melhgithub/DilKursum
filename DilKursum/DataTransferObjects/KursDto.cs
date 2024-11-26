using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace DilKursum.DataTransferObjects
{
    public class KursDto
    {
        public int ID { get; set; }
        public string KursAdi { get; set; }
        public string KursAciklama { get; set; }
        public int DersSayisi { get; set; }
        public decimal Fiyat { get; set; }
        public KursDurum Durum { get; set; }

        //Bağlantılar


        public List<string> Diller { get; set; }

        public string DilAdi { get; set; }
        public List<string> Egitmenler { get; set; }
        public string EgitmenKullaniciAdi { get; set; }

        public int EgitmenID { get; set; }
        public int DilID { get; set; }
    }
}
