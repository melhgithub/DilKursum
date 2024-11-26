using EntityLayer.Concrete;

namespace DilKursum.DataTransferObjects
{
    public class KursiyerKursDetailsDto
    {
        public int ID { get; set; }
        public KursiyerKursDetailDurum Durum { get; set; }
        public int KursID { get; set; }
        public Kurs Kurs { get; set; }
        public int KursiyerID { get; set; }
        public Kursiyer Kursiyer { get; set; }
        public int EgitmenID { get; set; }
        public Kursiyer Egitmen { get; set; }
        public int DilID { get; set; }
        public Kursiyer Dil { get; set; }

        public List<string> Egitmenler { get; set; }
        public List<string> Kurslar { get; set; }
        public List<string> Diller { get; set; }
        public List<string> Kursiyerler { get; set; }
    }
}
