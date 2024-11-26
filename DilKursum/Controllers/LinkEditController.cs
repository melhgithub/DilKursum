using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class LinkEditController : Controller
    {
        LinkManager linkManager = new LinkManager(new EFLinkRepository());
        public async Task<IActionResult> Index()
        {
            var links = await linkManager.GetList();
            var dto = new LinkEditDto();
            var model = new LinkEditViewModel
            {
                Dto = dto,
                Link = links
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LinkEditDto link)
        {
            if (ModelState.IsValid)
            {
                var updatedLink = await linkManager.GetByID(link.ID);
                if (updatedLink != null)
                {
                    updatedLink.Linkedin = link.Linkedin;
                    updatedLink.Whatsapp = link.Whatsapp;
                    updatedLink.Youtube = link.Youtube;
                    updatedLink.Gmail = link.Gmail;
                    updatedLink.Tiktok = link.Tiktok;
                    updatedLink.Telegram = link.Telegram;
                    if (link.LinkedinStatus == "2")
                    {
                        updatedLink.LinkedinStatus = false;
                    }
                    else
                    {
                        updatedLink.LinkedinStatus = true;
                    }
                    if (link.YoutubeStatus == "2")
                    {
                        updatedLink.YoutubeStatus = false;
                    }
                    else
                    {
                        updatedLink.YoutubeStatus = true;
                    }
                    if (link.WhatsappStatus == "2")
                    {
                        updatedLink.WhatsappStatus = false;
                    }
                    else
                    {
                        updatedLink.WhatsappStatus = true;
                    }
                    if (link.GmailStatus == "2")
                    {
                        updatedLink.GmailStatus = false;
                    }
                    else
                    {
                        updatedLink.GmailStatus = true;
                    }
                    if (link.TiktokStatus == "2")
                    {
                        updatedLink.TiktokStatus = false;
                    }
                    else
                    {
                        updatedLink.TiktokStatus = true;
                    }
                    if (link.TelegramStatus == "2")
                    {
                        updatedLink.TelegramStatus = false;
                    }
                    else
                    {
                        updatedLink.TelegramStatus = true;
                    }

                    await linkManager.Update(updatedLink);
                }
                return RedirectToAction("Index");
            }


            var filter = new LinkEditDto();
            return View("Index", new LinkEditViewModel
            {
                Dto = filter,
                Link = await linkManager.GetList(),
            });
        }
    }
}
