using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum KursiyerKursDetailDurum
    {

        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]

        Removed = 2
    }
    public class KursiyerKursDetail
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public KursiyerKursDetailDurum Durum { get; set; }

        //Bağlantılar
        public int KursID { get; set; }
        public Kurs Kurs { get; set; }
        public int KursiyerID { get; set; }
        public Kursiyer Kursiyer { get; set; }
    }
}
