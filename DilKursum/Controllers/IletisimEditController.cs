using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class IletisimEditController : Controller
    {
        IletisimManager iletisimManager = new IletisimManager(new EFIletisimRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());

        public async Task<IActionResult> Index()
        {
            var iletisim = await iletisimManager.GetList();
            var image = await imageManager.GetList();
            var filter = new IletisimEditDto();
            var model = new IletisimAdminViewModel
            {
                Dto = filter,
                Iletisim = iletisim,
                Images = image
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IletisimEditDto iletisim)
        {
            try
            {
                var updatedPage = await iletisimManager.GetByID(iletisim.ID);
                if (updatedPage != null)
                {
                    updatedPage.Baslik = iletisim.Baslik;
                    updatedPage.İlkyazi = iletisim.İlkyazi;
                    //if (hakkimizda.Image1 != null)
                    //{
                    //    int imageid = int.Parse(hakkimizda.Image1);
                    //    var image = await imageManager.GetByID(imageid);
                    //    if (image.Name != updatedPage.Image1)
                    //    {
                    //        updatedPage.Image1 = image.Name;
                    //    }
                    //}
                    //if (hakkimizda.Image2 == "null")
                    //{
                    //    updatedPage.Image2 = null;
                    //}
                    //else if (hakkimizda.Image2 != null && hakkimizda.Image2 != "null")
                    //{
                    //    int imageid = int.Parse(hakkimizda.Image2);
                    //    var image = await imageManager.GetByID(imageid);
                    //    updatedPage.Image2 = image.Name;
                    //}

                    //if (hakkimizda.Image3 == "null")
                    //{
                    //    updatedPage.Image3 = null;
                    //}
                    //else if (hakkimizda.Image3 != null && hakkimizda.Image3 != "null")
                    //{
                    //    int imageid = int.Parse(hakkimizda.Image3);
                    //    var image = await imageManager.GetByID(imageid);
                    //    updatedPage.Image3 = image.Name;
                    //}


                    await iletisimManager.Update(updatedPage);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                var dto = new IletisimEditDto();
                return View("Index", new IletisimAdminViewModel
                {
                    Dto = dto,
                    Iletisim = await iletisimManager.GetList(),
                    Images = await imageManager.GetList()
                });
            }
            return RedirectToAction("Index");
        }
    }
}
