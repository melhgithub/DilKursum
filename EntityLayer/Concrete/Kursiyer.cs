using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum KursiyerDurum
    {

        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]

        Removed = 2
    }
    public class Kursiyer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string KullaniciAdi { get; set; }

        [Required]
        [MaxLength(30)]
        public string Isim { get; set; }

        [Required]
        [MaxLength(30)]
        public string Soyisim { get; set; }
        [Required]
        [MaxLength(100)]
        public string Sifre { get; set; }
        [Required]
        public decimal Bakiye { get; set; }
        [Required]
        public KursiyerDurum Durum { get; set; }

        public string? FotoğrafUrl { get; set; }

    }
}
