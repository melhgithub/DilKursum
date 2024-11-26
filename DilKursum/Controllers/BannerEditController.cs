using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using MADEO_HFT.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class BannerEditController : Controller
    {
        BannerManager bannerManager = new BannerManager(new EFBannerRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());
        public async Task<IActionResult> Index()
        {
            var banner = await bannerManager.GetList();
            var image = await imageManager.GetList();
            var filter = new BannerEditDto();
            var model = new BannerAdminViewModel
            {
                Dto = filter,
                Banner = banner,
                Images = image
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(BannerEditDto banner)
        {
            try
            {
                var updatedPage = await bannerManager.GetByID(banner.ID);
                if (updatedPage != null)
                {
                    if (banner.Banner1 != null)
                    {

                        int imageid = int.Parse(banner.Banner1);
                        var image = await imageManager.GetByID(imageid);
                        updatedPage.Banner1 = image.Name;
                    }

                    if (banner.Banner2 != null)
                    {
                        int imageid = int.Parse(banner.Banner2);
                        var image = await imageManager.GetByID(imageid);
                        updatedPage.Banner2 = image.Name;
                    }
                    if (banner.Banner3 != null)
                    {

                        int imageid = int.Parse(banner.Banner3);
                        var image = await imageManager.GetByID(imageid);
                        updatedPage.Banner3 = image.Name;
                    }

                    await bannerManager.Update(updatedPage);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                var filter = new BannerEditDto();
                return View("Index", new BannerAdminViewModel
                {
                    Dto = filter,
                    Banner = await bannerManager.GetList(),
                    Images = await imageManager.GetList()
                });
            }
            return RedirectToAction("Index");
        }
    }
}
