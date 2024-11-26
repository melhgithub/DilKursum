using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DilKursum.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager adminManager = new AdminManager(new EFAdminRepository());
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());

        public async Task<IActionResult> Index()
        {
            var admins = await adminManager.GetList();

            var filter = new AdminLoginDto();

            var model = new AdminsLoginViewModel
            {
                Admins = admins,
                LoginDto = filter
            };


            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(AdminLoginDto admin)
        {
            var data = await adminManager.GetList();
            data = data.Where(x => x.KullaniciAdi == admin.UserName && x.Password == admin.Password).ToList();

            if (data.Count != 0)
            {
                HttpContext.Session.SetString("username", admin.UserName);

                if (admin.UserName == "admin" || admin.UserName == "melh")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,admin.UserName),
                        new Claim(ClaimTypes.Role, "admin")
                    };

                    var useridentity = new ClaimsIdentity(claims, "a");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

                    await HttpContext.SignInAsync(principal);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Admin") });
                }
                else
                {
                    await HttpContext.SignOutAsync();
                    HttpContext.Session.Clear();

                    return Json(new { success = false, message = "Geçersiz yetki ya da kullanıcı." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Geçersiz yetki ya da kullanıcı." });
            }


        }


        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> KursiyerLogin(EgitmenKursiyerLoginDto loginDto)
        {
            var kursiyerler = await kursiyerManager.GetList();
            var kursiyer = kursiyerler.FirstOrDefault(p => p.KullaniciAdi == loginDto.KullaniciAdi && p.Sifre == loginDto.Sifre);
            if (kursiyer != null && kursiyer.Durum == (KursiyerDurum)1)
            {
                HttpContext.Session.SetString("username", loginDto.KullaniciAdi);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, kursiyer.KullaniciAdi),
                    new Claim(ClaimTypes.Role, "kursiyer"),
                };

                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

                await HttpContext.SignInAsync(principal);

                return Json(new { success = true, message = "Giriş başarılı!", redirectUrl = Url.Action("Index", "Profil") });

            }
            else if (kursiyer != null && kursiyer.Durum == (KursiyerDurum)2)
            {
                return Json(new { success = false, message = "Üyeliğiniz aktif değildir." });
            }
            else
            {
                return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre." });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EgitmenLogin(EgitmenKursiyerLoginDto loginDto)
        {
            var egitmenler = await egitmenManager.GetList();

            var egitmen = egitmenler.FirstOrDefault(p => p.KullaniciAdi == loginDto.KullaniciAdi && p.Sifre == loginDto.Sifre);

            if (egitmen != null && egitmen.Durum == (EgitmenDurum)1)
            {
                HttpContext.Session.SetString("username", loginDto.KullaniciAdi);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, egitmen.KullaniciAdi),
                    new Claim(ClaimTypes.Role, "egitmen"),
                };

                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

                await HttpContext.SignInAsync(principal);

                return Json(new { success = true, message = "Giriş başarılı!", redirectUrl = Url.Action("Index", "Profil") });

            }
            else if (egitmen != null && egitmen.Durum == (EgitmenDurum)2)
            {
                return Json(new { success = false, message = "Üyeliğiniz aktif değildir." });
            }
            else
            {
                return Json(new { success = false, message = "Geçersiz kullanıcı adı veya şifre." });
            }
        }

    }
}
