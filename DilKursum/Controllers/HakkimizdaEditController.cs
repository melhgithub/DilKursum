using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class HakkimizdaEditController : Controller
    {
        HakkimizdaManager hakkimizdaManager = new HakkimizdaManager(new EFHakkimizdaRepository());
        ImageManager imageManager = new ImageManager(new EFImageRepository());

        public async Task<IActionResult> Index()
        {
            var hakkimizda = await hakkimizdaManager.GetList();
            var image = await imageManager.GetList();
            var filter = new HakkimizdaEditDto();
            var model = new HakkimizdaAdminViewModel
            {
                Dto = filter,
                Hakkimizda = hakkimizda,
                Images = image
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HakkimizdaEditDto hakkimizda)
        {
            try
            {
                var updatedPage = await hakkimizdaManager.GetByID(hakkimizda.ID);
                if (updatedPage != null)
                {
                    updatedPage.Baslik = hakkimizda.Baslik;
                    updatedPage.İlkyazi = hakkimizda.İlkyazi;
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


                    await hakkimizdaManager.Update(updatedPage);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                var dto = new HakkimizdaEditDto();
                return View("Index", new HakkimizdaAdminViewModel
                {
                    Dto = dto,
                    Hakkimizda = await hakkimizdaManager.GetList(),
                    Images = await imageManager.GetList()
                });
            }
            return RedirectToAction("Index");
        }
    }
}
