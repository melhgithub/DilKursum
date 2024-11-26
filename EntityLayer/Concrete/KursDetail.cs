using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum KursDetailDurum
    {
        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]

        Removed = 2
    }
    public class KursDetail
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(350)]
        public string? VideoLink { get; set; }
        [MaxLength(350)]
        public string? ImageLink { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public KursDetailDurum Durum { get; set; }

        //Bağlantılar
        public int KursID { get; set; }
        public Kurs Kurs { get; set; }
    }
}
