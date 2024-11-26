namespace DilKursum.Models
{
    public class KursFileEditViewModel
    {
        public int EgitmenID { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public bool IsVideo { get; set; }
        public IFormFile File { get; set; }
    }
}
