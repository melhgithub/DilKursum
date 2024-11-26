using EntityLayer.Concrete;

namespace DilKursum.Models
{
    public class HeaderModel
    {
        public List<Link> Links { get; set; }
        public List<Header> Headers { get; set; }
        public List<Banner> Banner { get; set; }
        public bool IsLoggedIn { get; set; }
        public UserProfileForHeader UserProfile { get; set; }
    }
}
