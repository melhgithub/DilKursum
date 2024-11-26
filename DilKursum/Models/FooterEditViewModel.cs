using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class FooterEditViewModel
    {
        public FooterEditDto Dto { get; set; }
        public List<Footer> Footer { get; set; }
    }
}
