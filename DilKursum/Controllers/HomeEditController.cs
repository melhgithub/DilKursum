using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class HomeEditController : Controller
    {
        AnasayfaManager anasayfaManager = new AnasayfaManager(new EFAnasayfaRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());

        public async Task<IActionResult> Index()
        {
            var anasayfa = await anasayfaManager.GetList();
            var image = await imageManager.GetList();
            var filter = new AnasayfaEditDto();
            var model = new AnasayfaAdminViewModel
            {
                Dto = filter,
                Anasayfa = anasayfa,
                Images = image
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AnasayfaEditDto homepage)
        {
            try
            {
                var updatedPage = await anasayfaManager.GetByID(homepage.ID);
                if (updatedPage != null)
                {
                    updatedPage.Baslik = homepage.Baslik;
                    updatedPage.İlkyazi = homepage.İlkyazi;
                    //if (homepage.Image1 != null)
                    //{
                    //    int imageid = int.Parse(homepage.Image1);
                    //    var image = await imageManager.GetByID(imageid);
                    //    if (image.Name != updatedPage.Image1)
                    //    {
                    //        updatedPage.Image1 = image.Name;
                    //    }
                    //}
                    //if (homepage.Image2 == "null")
                    //{
                    //    updatedPage.Image2 = null;
                    //}
                    //else if (homepage.Image2 != null && homepage.Image2 != "null")
                    //{
                    //    int imageid = int.Parse(homepage.Image2);
                    //    var image = await imageManager.GetByID(imageid);
                    //    updatedPage.Image2 = image.Name;
                    //}

                    //if (homepage.Image3 == "null")
                    //{
                    //    updatedPage.Image3 = null;
                    //}
                    //else if (homepage.Image3 != null && homepage.Image3 != "null")
                    //{
                    //    int imageid = int.Parse(homepage.Image3);
                    //    var image = await imageManager.GetByID(imageid);
                    //    updatedPage.Image3 = image.Name;
                    //}


                    await anasayfaManager.Update(updatedPage);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                var dto = new AnasayfaEditDto();
                return View("Index", new AnasayfaAdminViewModel
                {
                    Dto = dto,
                    Anasayfa = await anasayfaManager.GetList(),
                    Images = await imageManager.GetList()
                });
            }
            return RedirectToAction("Index");
        }
    }
}
