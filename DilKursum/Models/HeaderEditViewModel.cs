using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class HeaderEditViewModel
    {
        public HeaderEditDto Dto { get; set; }
        public List<Header> Header { get; set; }
    }
}
