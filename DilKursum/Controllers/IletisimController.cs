using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [AllowAnonymous]
    public class IletisimController : Controller
    {
        IletisimManager iletisimManager = new IletisimManager(new EFIletisimRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());
        BannerManager bannerManager = new BannerManager(new EFBannerRepository());

        public async Task<IActionResult> Index()
        {
            var iletisim = await iletisimManager.GetList();
            var images = await imageManager.GetList();
            var banners = await bannerManager.GetList();

            var model = new IletisimViewModel
            {
                Iletisim = iletisim,
                Images = images,
                Banners = banners
            };


            return View(model);
        }
    }
}
