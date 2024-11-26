using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class KurumsalEditController : Controller
    {
        KurumsalManager kurumsalManager = new KurumsalManager(new EFKurumsalRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());

        public async Task<IActionResult> Index()
        {
            var kurumsal = await kurumsalManager.GetList();
            var image = await imageManager.GetList();
            var filter = new KurumsalEditDto();
            var model = new KurumsalAdminViewModel
            {
                Dto = filter,
                Kurumsal = kurumsal,
                Images = image
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KurumsalEditDto kurumsal)
        {
            try
            {
                var updatedPage = await kurumsalManager.GetByID(kurumsal.ID);
                if (updatedPage != null)
                {
                    updatedPage.Baslik = kurumsal.Baslik;
                    updatedPage.İlkyazi = kurumsal.İlkyazi;
                    //if (kurumsal.Image1 != null)
                    //{
                    //    int imageid = int.Parse(kurumsal.Image1);
                    //    var image = await imageManager.GetByID(imageid);
                    //    if (image.Name != updatedPage.Image1)
                    //    {
                    //        updatedPage.Image1 = image.Name;
                    //    }
                    //}
                    //if (kurumsal.Image2 == "null")
                    //{
                    //    updatedPage.Image2 = null;
                    //}
                    //else if (kurumsal.Image2 != null && kurumsal.Image2 != "null")
                    //{
                    //    int imageid = int.Parse(kurumsal.Image2);
                    //    var image = await imageManager.GetByID(imageid);
                    //    updatedPage.Image2 = image.Name;
                    //}

                    //if (kurumsal.Image3 == "null")
                    //{
                    //    updatedPage.Image3 = null;
                    //}
                    //else if (kurumsal.Image3 != null && kurumsal.Image3 != "null")
                    //{
                    //    int imageid = int.Parse(kurumsal.Image3);
                    //    var image = await imageManager.GetByID(imageid);
                    //    updatedPage.Image3 = image.Name;
                    //}


                    await kurumsalManager.Update(updatedPage);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                var dto = new KurumsalEditDto();
                return View("Index", new KurumsalAdminViewModel
                {
                    Dto = dto,
                    Kurumsal = await kurumsalManager.GetList(),
                    Images = await imageManager.GetList()
                });
            }
            return RedirectToAction("Index");
        }
    }
}
