using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DilKursum.Models;
using System.Security.Cryptography;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class KursFileEditController : Controller
    {
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursFileManager kursFileManager = new KursFileManager(new EFKursFileRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());

        private readonly IWebHostEnvironment _env;
        public KursFileEditController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var egitmenler = await egitmenManager.GetList();
            return View(egitmenler);
        }

        public async Task<ActionResult> Dosyalar(int egitmenId)
        {
            var alldosyalar = await kursFileManager.GetListWithIncludesAsync();
            var dosyalar = alldosyalar.Where(p => p.EgitmenID == egitmenId).ToList();
            ViewBag.EgitmenId = egitmenId;
            return PartialView("_DosyaListesi", dosyalar);
        }

        [HttpGet]
        public async Task<IActionResult> GetDosyaEditForm(int id)
        {
            var dosya = await kursFileManager.GetByID(id);
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


        public async Task<IActionResult> YeniDosya(int EgitmenID, string fileName, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Lütfen geçerli bir dosya seçin.";
                return RedirectToAction("Index");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
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

        private bool IsVideo(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var videoExtensions = new[] { ".mp4", ".avi", ".mkv", ".mov" };
            return videoExtensions.Contains(fileExtension);
        }



        [HttpPost]
        public async Task<ActionResult> DosyaSil(int dosyaId, int egitmenId)
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

                    var kursDetaylari = await kursDetailManager.GetListWithIncludesAsync(); // Kurs detayları alınır

                    foreach (var detay in kursDetaylari)
                    {
                        if (detay.Kurs.EgitmenID == egitmenId)
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

        [HttpPost]
        public async Task<ActionResult> DosyaDuzenle(int dosyaId, string fileName, string name, IFormFile file)
        {
            var dosya = await kursFileManager.GetByID(dosyaId);

            if (dosya != null)
            {
                if (file != null)
                {
                    var eskiDosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", name);
                    if (System.IO.File.Exists(eskiDosyaYolu))
                    {
                        System.IO.File.Delete(eskiDosyaYolu);
                    }

                    var yeniDosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var yeniDosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", yeniDosyaAdi);

                    using (var stream = new FileStream(yeniDosyaYolu, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    dosya.IsVideo = IsVideo(file);
                    dosya.Name = yeniDosyaAdi;
                }

                dosya.FileName = fileName;

                await kursFileManager.Update(dosya);

                TempData["SuccessMessage"] = "Dosya başarıyla güncellendi!";
            }

            return RedirectToAction("Index");
        }






    }
}
