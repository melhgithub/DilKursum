using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class KursFile
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public int EgitmenID { get; set; }
        public Egitmen Egitmen { get; set; }

        [Required]
        public bool IsVideo { get; set; }
    }
}
