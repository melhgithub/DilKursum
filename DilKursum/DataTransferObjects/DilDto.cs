using EntityLayer.Concrete;

namespace DilKursum.DataTransferObjects
{
    public class DilDto
    {
        public int ID { get; set; }
        public string DilAdi { get; set; }
        public DilDurum Durum { get; set; }
    }
}
