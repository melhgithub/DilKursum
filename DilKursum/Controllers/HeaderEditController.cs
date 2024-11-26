using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class HeaderEditController : Controller
    {
        HeaderManager headerManager = new HeaderManager(new EFHeaderRepository());

        public async Task<IActionResult> Index()
        {
            var header = await headerManager.GetList();
            var dto = new HeaderEditDto();

            var model = new HeaderEditViewModel
            {
                Header = header,
                Dto = dto
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HeaderEditDto header)
        {
            if (ModelState.IsValid)
            {
                var updatedHeader = await headerManager.GetByID(header.ID);
                if (updatedHeader != null)
                {
                    updatedHeader.Menu1Link = header.Menu1Link;
                    updatedHeader.Menu2Link = header.Menu2Link;
                    updatedHeader.Menu3Link = header.Menu3Link;
                    updatedHeader.Menu4Link = header.Menu4Link;
                    updatedHeader.Menu5Link = header.Menu5Link;
                    updatedHeader.Menu6Link = header.Menu6Link;
                    updatedHeader.Menu7Link = header.Menu7Link;
                    updatedHeader.Menu8Link = header.Menu8Link;
                    updatedHeader.Menu9Link = header.Menu9Link;
                    updatedHeader.Menu1 = header.Menu1;
                    updatedHeader.Menu2 = header.Menu2;
                    updatedHeader.Menu3 = header.Menu3;
                    updatedHeader.Menu4 = header.Menu4;
                    updatedHeader.Menu5 = header.Menu5;
                    updatedHeader.Menu6 = header.Menu6;
                    updatedHeader.Menu7 = header.Menu7;
                    updatedHeader.Menu8 = header.Menu8;
                    updatedHeader.Menu9 = header.Menu9;
                    updatedHeader.Menu10 = header.Menu10;
                    updatedHeader.Menu11 = header.Menu11;
                    updatedHeader.Menu12 = header.Menu12;
                    updatedHeader.Menu13 = header.Menu13;
                    updatedHeader.Menu14 = header.Menu14;
                    updatedHeader.Menu15 = header.Menu15;
                    for (int i = 1; i <= 15; i++)
                    {
                        string statusProperty = $"Menu{i}Status";
                        string statusValue = (string)header.GetType().GetProperty(statusProperty)?.GetValue(header);

                        if (statusValue == "2")
                        {
                            updatedHeader.GetType().GetProperty(statusProperty)?.SetValue(updatedHeader, false);
                        }
                        else
                        {
                            updatedHeader.GetType().GetProperty(statusProperty)?.SetValue(updatedHeader, true);
                        }
                    }

                    await headerManager.Update(updatedHeader);
                }
                return RedirectToAction("Index");
            }


            var dto = new HeaderEditDto();
            return View("Index", new HeaderEditViewModel
            {
                Dto = dto,
                Header = await headerManager.GetList(),
            });
        }
    }
}
