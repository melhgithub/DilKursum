using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [AllowAnonymous]
    public class KurumsalController : Controller
    {
        KurumsalManager kurumsalManager = new KurumsalManager(new EFKurumsalRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());
        BannerManager bannerManager = new BannerManager(new EFBannerRepository());

        public async Task<IActionResult> Index()
        {
            var kurumsal = await kurumsalManager.GetList();
            var images = await imageManager.GetList();
            var banners = await bannerManager.GetList();

            var model = new KurumsalViewModel
            {
                Kurumsal = kurumsal,
                Images = images,
                Banners = banners
            };


            return View(model);
        }
    }
}
