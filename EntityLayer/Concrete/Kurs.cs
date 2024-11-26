using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum KursDurum
    {
        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]

        Removed = 2
    }
    public class Kurs
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string KursAdi { get; set; }
        [Required]
        [MaxLength(500)]
        public string KursAciklama { get; set; }
        [Required]
        public int DersSayisi { get; set; }
        [Required]
        public decimal Fiyat { get; set; }
        [Required]
        public KursDurum Durum { get; set; }

        //Bağlantılar

        public int EgitmenID { get; set; }
        public Egitmen Egitmen { get; set; }
        public int DilID { get; set; }
        public Dil Dil { get; set; }

    }
}
