﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Footer
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public bool ButtonStatus { get; set; }
    }
}
