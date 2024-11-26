using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class FooterEditController : Controller
    {
        FooterManager footerManager = new FooterManager(new EFFooterRepository());

        public async Task<IActionResult> Index()
        {
            var footer = await footerManager.GetList();
            var filter = new FooterEditDto();

            var model = new FooterEditViewModel
            {
                Footer = footer,
                Dto = filter
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FooterEditDto footer)
        {
            try
            {
                var updatedFooter = await footerManager.GetByID(footer.ID);
                if (updatedFooter != null)
                {
                    updatedFooter.Title = footer.Title;
                    updatedFooter.Content = footer.Content;
                    updatedFooter.ButtonLink = footer.ButtonLink;
                    updatedFooter.ButtonText = footer.ButtonText;
                    if (footer.ButtonStatus == "1")
                    {
                        updatedFooter.ButtonStatus = true;
                    }
                    else
                    {
                        updatedFooter.ButtonStatus = false;
                    }

                    await footerManager.Update(updatedFooter);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                var filter = new FooterEditDto();
                return View("Index", new FooterEditViewModel
                {
                    Dto = filter,
                    Footer = await footerManager.GetList(),
                });
            }
        }
    }
}
