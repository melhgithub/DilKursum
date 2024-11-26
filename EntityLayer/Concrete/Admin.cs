using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string KullaniciAdi { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
