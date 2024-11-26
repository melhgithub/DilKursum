using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DilKursum.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        AnasayfaManager anasayfaManager = new AnasayfaManager(new EFAnasayfaRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());
        BannerManager bannerManager = new BannerManager(new EFBannerRepository());

        public async Task<IActionResult> Index()
        {
            var anasayfa = await anasayfaManager.GetList();
            var images = await imageManager.GetList();
            var banners = await bannerManager.GetList();

            var model = new AnasayfaViewModel
            {
                Anasayfa = anasayfa,
                Images = images,
                Banners = banners
            };


            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
