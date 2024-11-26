using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum DilDurum

    {
        [Description("AKTİF")]
        Approved = 1,

        [Description("KALDIRILDI")]
        Removed = 2

    }
    public class Dil
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string DilAdi { get; set; }

        [Required]
        public DilDurum Durum { get; set; }


    }
}
