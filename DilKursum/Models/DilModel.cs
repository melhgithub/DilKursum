using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class DilModel
    {
        public DilDto DTO { get; set; }
        public List<Dil> Diller { get; set; }
    }
}
