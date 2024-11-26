using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [AllowAnonymous]
    public class HakkimizdaController : Controller
    {
        HakkimizdaManager hakkimizdaManager = new HakkimizdaManager(new EFHakkimizdaRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());
        BannerManager bannerManager = new BannerManager(new EFBannerRepository());

        public async Task<IActionResult> Index()
        {
            var hakkimizda = await hakkimizdaManager.GetList();
            var images = await imageManager.GetList();
            var banners = await bannerManager.GetList();

            var model = new HakkimizdaViewModel
            {
                Hakkimizda = hakkimizda,
                Images = images,
                Banners = banners
            };


            return View(model);
        }
    }
}
