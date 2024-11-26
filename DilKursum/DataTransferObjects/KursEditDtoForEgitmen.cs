using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace DilKursum.DataTransferObjects
{
    public class KursEditDtoForEgitmen
    {
        public int ID { get; set; }
        public string KursAdi { get; set; }
        public string KursAciklama { get; set; }
        public int DersSayisi { get; set; }
        public decimal Fiyat { get; set; }
        public KursDurum Durum { get; set; }

        public int EgitmenID { get; set; }
        public Egitmen Egitmen { get; set; }
        public int DilID { get; set; }
        public Dil Dil { get; set; }
    }
}
