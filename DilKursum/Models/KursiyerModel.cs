using DilKursum.DataTransferObjects;
using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class KursiyerModel
    {
        public KursiyerDto DTO { get; set; }
        public List<Kursiyer> Kursiyerler { get; set; }

    }
}
