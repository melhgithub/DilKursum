using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DilKursum.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());

        public async Task<IActionResult> Index()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            object model = null;

            if (role == "egitmen")
            {
                var username = User.Identity.Name;
                model = await GetEgitmenDTO(username);
            }
            else if (role == "kursiyer")
            {
                var username = User.Identity.Name;
                model = await GetKursiyerDTO(username);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }

            if (model == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            return View(model);
        }



        private async Task<EgitmenDto> GetEgitmenDTO(string username)
        {
            var egitmenKadi = await egitmenManager.GetEgitmenIdByUserName(username);
            var egitmen = await egitmenManager.GetByID(egitmenKadi);

            if (egitmen == null)
            {
                RedirectToAction("Logout", "Login");
            }

            var DTO = new EgitmenDto
            {
                ID = egitmen.ID,
                Email = egitmen.Email,
                KullaniciAdi = egitmen.KullaniciAdi,
                Isim = egitmen.Isim,
                Soyisim = egitmen.Soyisim,
                Sifre = egitmen.Sifre,
                KisaBilgi = egitmen.KisaBilgi,
                Durum = egitmen.Durum,
                FotoğrafUrl = egitmen.FotoğrafUrl
            };

            return DTO;
        }

        private async Task<KursiyerDto> GetKursiyerDTO(string username)
        {
            var kursiyerKadi = await kursiyerManager.GetKursiyerIDByKursiyerName(username);
            var kursiyer = await kursiyerManager.GetByID(kursiyerKadi);

            if (kursiyer == null)
            {
                RedirectToAction("Logout", "Login");
            }

            var DTO = new KursiyerDto
            {
                ID = kursiyer.ID,
                Email = kursiyer.Email,
                KullaniciAdi = kursiyer.KullaniciAdi,
                Isim = kursiyer.Isim,
                Soyisim = kursiyer.Soyisim,
                Sifre = kursiyer.Sifre,
                Durum = kursiyer.Durum,
                FotoğrafUrl = kursiyer.FotoğrafUrl
            };

            return DTO;
        }


        [HttpPost]
        public async Task<IActionResult> Update(EgitmenKursiyerProfilDto DTO)
        {
            var response = new { success = false, message = "" };

            var existingKursiyer = await kursiyerManager.GetByID(DTO.ID);
            var existingEgitmen = await egitmenManager.GetByID(DTO.ID);

            if (existingKursiyer == null && existingEgitmen == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            if ((DTO.EskiSifre != null) && ((existingKursiyer != null && existingKursiyer.Sifre != DTO.EskiSifre) ||
                (existingEgitmen != null && existingEgitmen.Sifre != DTO.EskiSifre)))
            {
                return Json(new { success = false, message = "Eski Şifre Yanlış!" });
            }

            bool isPasswordProvided = !string.IsNullOrEmpty(DTO.Sifre);
            if ((DTO.EskiSifre != null) && (isPasswordProvided && DTO.Sifre != DTO.SifreTekrar))
            {
                return Json(new { success = false, message = "Şifreler Eşleşmiyor!" });
            }
            else if ((DTO.EskiSifre == null) && (isPasswordProvided && DTO.Sifre == DTO.SifreTekrar))
            {
                return Json(new { success = false, message = "Eski Şifre Boş!" });
            }

            string photoPath = null;


            if (existingKursiyer != null)
            {
                if (DTO.Sil)
                {
                    if (existingKursiyer.FotoğrafUrl != null)
                    {
                        var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingKursiyer.FotoğrafUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                        existingKursiyer.FotoğrafUrl = null;
                    }
                }
                else if (DTO.Fotoğraf != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(DTO.Fotoğraf.FileName);
                    var fileExtension = Path.GetExtension(DTO.Fotoğraf.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profil/kursiyer/photos", uniqueFileName);

                    if (existingKursiyer.FotoğrafUrl != null)
                    {
                        var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingKursiyer.FotoğrafUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                    }

                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await DTO.Fotoğraf.CopyToAsync(fileStream);
                    }

                    existingKursiyer.FotoğrafUrl = $"/profil/kursiyer/photos/{uniqueFileName}";
                }
                existingKursiyer.Isim = DTO.Isim;
                existingKursiyer.Soyisim = DTO.Soyisim;
                existingKursiyer.Email = DTO.Email;

                if (isPasswordProvided)
                {
                    existingKursiyer.Sifre = DTO.Sifre;
                }

                await kursiyerManager.Update(existingKursiyer);
                response = new { success = true, message = "Başarılı!" };
            }
            else if (existingEgitmen != null)
            {
                if (DTO.Sil)
                {
                    if (existingEgitmen.FotoğrafUrl != null)
                    {
                        var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingEgitmen.FotoğrafUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                        existingEgitmen.FotoğrafUrl = null;
                    }
                }
                else if (DTO.Fotoğraf != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(DTO.Fotoğraf.FileName);
                    var fileExtension = Path.GetExtension(DTO.Fotoğraf.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profil/egitmen/photos", uniqueFileName);

                    if (existingEgitmen.FotoğrafUrl != null)
                    {
                        var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingEgitmen.FotoğrafUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                    }

                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await DTO.Fotoğraf.CopyToAsync(fileStream);
                    }

                    existingEgitmen.FotoğrafUrl = $"/profil/egitmen/photos/{uniqueFileName}";
                }
                existingEgitmen.Isim = DTO.Isim;
                existingEgitmen.Soyisim = DTO.Soyisim;
                existingEgitmen.Email = DTO.Email;

                if (isPasswordProvided)
                {
                    existingEgitmen.Sifre = DTO.Sifre;
                }

                existingEgitmen.KisaBilgi = DTO.KisaBilgi;

                await egitmenManager.Update(existingEgitmen);
                response = new { success = true, message = "Başarılı!" };
            }

            return Json(response);
        }


    }
}
