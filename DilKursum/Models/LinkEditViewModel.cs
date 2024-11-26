using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class LinkEditViewModel
    {
        public LinkEditDto Dto { get; set; }
        public List<Link> Link { get; set; }
    }
}
