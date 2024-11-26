using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using Microsoft.AspNetCore.Mvc;

namespace MADEO_HFT.ViewComponents.Header
{
    public class Header : ViewComponent
    {
        LinkManager linkManager = new LinkManager(new EFLinkRepository());
        HeaderManager headerManager = new HeaderManager(new EFHeaderRepository());
        BannerManager bannerManager = new BannerManager(new EFBannerRepository());
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        AdminManager adminManager = new AdminManager(new EFAdminRepository());

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var link = await linkManager.GetList();
            var header = await headerManager.GetList();
            var banner = await bannerManager.GetList();

            var username = HttpContext.Session.GetString("username");
            UserProfileForHeader userProfile = null;

            if (!string.IsNullOrEmpty(username))
            {
                if (username == "melh" || username == "admin")
                {
                    userProfile = new UserProfileForHeader
                    {
                        Name = username,
                        ProfilePictureUrl = "belkiadminfotosugelir"
                    };
                }
                else
                {
                    var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
                    var egitmen = await egitmenManager.GetByID(egitmenID);
                    if (egitmen != null)
                    {
                        userProfile = new UserProfileForHeader
                        {
                            Name = egitmen.KullaniciAdi,
                            ProfilePictureUrl = egitmen.FotoğrafUrl
                        };
                    }

                    var kursiyerID = await kursiyerManager.GetKursiyerIDByKursiyerName(username);
                    var kursiyer = await kursiyerManager.GetByID(kursiyerID);
                    if (kursiyer != null)
                    {
                        userProfile = new UserProfileForHeader
                        {
                            Name = kursiyer.KullaniciAdi,
                            ProfilePictureUrl = kursiyer.FotoğrafUrl
                        };
                    }
                }
            }

            var model = new HeaderModel
            {
                Links = link,
                Headers = header,
                Banner = banner,
                IsLoggedIn = username != null,
                UserProfile = userProfile
            };
            ViewBag.Username = username;

            return View(model);
        }
    }
}
