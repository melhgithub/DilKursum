using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DilKursum.Controllers
{
    [Authorize]
    public class DosyalarimController : Controller
    {
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursFileManager kursFileManager = new KursFileManager(new EFKursFileRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        private readonly IWebHostEnvironment _env;
        public DosyalarimController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Authorize(Policy = "egitmen")]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("username");
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            if (role == "egitmen")
            {
                var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
                var egitmen = await egitmenManager.GetByID(egitmenID);
                var allfiles = await kursFileManager.GetListWithIncludesAsync();
                var files = allfiles.Where(p => p.EgitmenID == egitmenID).ToList();

                ViewBag.EgitmenID = egitmenID;

                var model = new DosyalarimModel
                {
                    Dosyalar = files,
                };

                return View("Index", model);
            }

            return RedirectToAction("Logout", "Login");
        }


        public async Task<ActionResult> Dosyalar(int egitmenId)
        {
            var username = HttpContext.Session.GetString("username");
            var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
            var alldosyalar = await kursFileManager.GetListWithIncludesAsync();
            var dosyalar = alldosyalar.Where(p => p.EgitmenID == egitmenId).ToList();

            if (egitmenID == egitmenId)
            {
                ViewBag.EgitmenId = egitmenId;
                return PartialView("_DosyaListesi", dosyalar);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetDosyaEditForm(int id)
        {
            var dosya = await kursFileManager.GetByID(id);
            var username = HttpContext.Session.GetString("username");
            var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
            if (egitmenID == dosya.EgitmenID)
            {
                if (dosya == null)
                {
                    return NotFound();
                }

                return Json(new
                {
                    fileName = dosya.FileName,
                    name = dosya.Name,
                    id = dosya.ID
                });
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public async Task<IActionResult> YeniDosya(int EgitmenID, string fileName, IFormFile file)
        {
            var username = HttpContext.Session.GetString("username");
            var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
            if (egitmenID == EgitmenID)
            {
                if (file == null || file.Length == 0)
                {
                    TempData["ErrorMessage"] = "Lütfen geçerli bir dosya seçin.";
                    return RedirectToAction("Index");
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uniqueFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Dosya yüklenirken bir hata oluştu: {ex.Message}";
                    return RedirectToAction("Index");
                }

                var dosya = new KursFile
                {
                    EgitmenID = EgitmenID,
                    Name = uniqueFileName,
                    FileName = fileName,
                    IsVideo = IsVideo(file),
                };

                await kursFileManager.Add(dosya);
                TempData["SuccessMessage"] = "Yeni dosya başarıyla eklendi!";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }


        private bool IsVideo(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var videoExtensions = new[] { ".mp4", ".avi", ".mkv", ".mov" };
            return videoExtensions.Contains(fileExtension);
        }




        [HttpPost]
        public async Task<ActionResult> DosyaSil(int dosyaId, int egitmenId)
        {
            var username = HttpContext.Session.GetString("username");
            var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
            if (egitmenID == egitmenId)
            {
                var dosya = await kursFileManager.GetByID(dosyaId);
                if (dosya != null)
                {
                    try
                    {
                        string filePath = Path.Combine(_env.WebRootPath, "uploads", dosya.Name);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        var kursDetaylari = await kursDetailManager.GetListWithIncludesAsync();

                        foreach (var detay in kursDetaylari)
                        {
                            if (detay.Kurs.EgitmenID == egitmenID)
                            {
                                if (detay.VideoLink == dosya.Name)
                                {
                                    detay.VideoLink = null;
                                }

                                if (detay.ImageLink == dosya.Name)
                                {
                                    detay.ImageLink = null;
                                }

                                await kursDetailManager.Update(detay);
                            }
                        }


                        await kursFileManager.Delete(dosya);

                        TempData["SuccessMessage"] = "Dosya başarıyla silindi!";
                    }
                    catch (Exception)
                    {
                        TempData["ErrorMessage"] = "Dosya silinirken bir hata oluştu.";
                    }
                }

                return RedirectToAction("Index");


            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DosyaDuzenle(int dosyaId, string fileName, string name, IFormFile file)
        {
            var dosya = await kursFileManager.GetByID(dosyaId);
            var username = HttpContext.Session.GetString("username");
            var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);
            if (egitmenID == dosya.EgitmenID)
            {
                if (dosya != null)
                {
                    if (file != null)
                    {
                        var eskiDosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", dosya.Name);
                        if (System.IO.File.Exists(eskiDosyaYolu))
                        {
                            System.IO.File.Delete(eskiDosyaYolu);
                        }

                        var yeniDosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var yeniDosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", yeniDosyaAdi);

                        try
                        {
                            using (var stream = new FileStream(yeniDosyaYolu, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            dosya.IsVideo = IsVideo(file);
                            dosya.Name = yeniDosyaAdi;
                        }
                        catch (Exception ex)
                        {
                            TempData["ErrorMessage"] = $"Dosya düzenlenirken bir hata oluştu: {ex.Message}";
                            return RedirectToAction("Index");
                        }
                    }

                    dosya.FileName = fileName;

                    await kursFileManager.Update(dosya);

                    TempData["SuccessMessage"] = "Dosya başarıyla güncellendi!";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }



    }
}
