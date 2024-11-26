using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [AllowAnonymous]
    public class KurslarController : Controller
    {
        DilManager dilManager = new DilManager(new EFDilRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        KursFileManager kursFileManager = new KursFileManager(new EFKursFileRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        public async Task<IActionResult> Index()
        {
            var diller = await dilManager.GetList();
            diller = diller.Where(p => p.Durum == (DilDurum)1).ToList();
            var kurslar = await kursManager.GetListWithIncludesAsync();
            kurslar = kurslar.Where(p => p.Durum == (KursDurum)1 && p.Egitmen.Durum == (EgitmenDurum)1 && p.Dil.Durum == (DilDurum)1).ToList();
            var files = await kursFileManager.GetListWithIncludesAsync();
            files = files.Where(p => p.Egitmen.Durum == (EgitmenDurum)1).ToList();
            var allDetails = await kursDetailManager.GetListWithIncludesAsync();
            allDetails = allDetails.Where(p => p.Kurs.Durum == (KursDurum)1 && p.Kurs.Egitmen.Durum == (EgitmenDurum)1).ToList();
            var egitmenler = await egitmenManager.GetList();
            egitmenler = egitmenler.Where(p => p.Durum == (EgitmenDurum)1).ToList();

            var Dto = new KursFilterDto();
            var model = new KurslarViewModel
            {
                DTO = Dto,
                Diller = diller,
                Kurslar = kurslar,
                KursDetaylari = allDetails,
                Egitmenler = egitmenler,
                Dosyalar = files
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterKurslar(KursFilterDto filterDto)
        {
            var kurslar = await kursManager.GetListWithIncludesAsync();

            kurslar = kurslar.Where(p => p.Durum == (KursDurum)1 && p.Egitmen.Durum == (EgitmenDurum)1 && p.Dil.Durum == (DilDurum)1).ToList();

            if (!string.IsNullOrEmpty(filterDto.KursAdi))
            {
                var kursAdiLower = filterDto.KursAdi.ToLower();
                kurslar = kurslar.Where(k => k.KursAdi.ToLower().Contains(kursAdiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(filterDto.EgitmenKullaniciAdi))
            {
                var egitmenKullaniciAdiLower = filterDto.EgitmenKullaniciAdi.ToLower();
                kurslar = kurslar.Where(k => k.Egitmen.KullaniciAdi.ToLower().Contains(egitmenKullaniciAdiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(filterDto.DilAdi))
            {
                var dilAdiLower = filterDto.DilAdi.ToLower();
                kurslar = kurslar.Where(k => k.Dil.DilAdi.ToLower().Contains(dilAdiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(filterDto.Fiyat.ToString()) && filterDto.Fiyat.ToString() != "0")
            {
                var fiyatStr = filterDto.Fiyat.ToString();
                kurslar = kurslar.Where(k => k.Fiyat.ToString().Contains(fiyatStr)).ToList();
            }


            if (filterDto.DersSayisi > 0)
            {
                var dersSayiStr = filterDto.DersSayisi.ToString();
                kurslar = kurslar.Where(k => k.DersSayisi.ToString().Contains(dersSayiStr)).ToList();
            }

            var result = kurslar.Select(k => new
            {
                k.ID,
                k.KursAdi,
                k.KursAciklama,
                k.Egitmen.KullaniciAdi,
                k.DersSayisi,
                k.Fiyat,
                k.Dil.DilAdi
            }).ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetKursDetay(int id)
        {
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var kurs = kurslar.FirstOrDefault(k => k.ID == id && k.Durum == KursDurum.Approved && k.Egitmen.Durum == (EgitmenDurum)1);

            if (kurs == null)
            {
                return NotFound();
            }

            var kursDetaylar = await kursDetailManager.GetListWithIncludesAsync();
            var ilgiliKursDetaylari = kursDetaylar
                .Where(d => d.KursID == id && d.Durum == KursDetailDurum.Approved)
                .Select(d => new
                {
                    ID = d.ID,
                    Description = d.Description,
                    VideoLink = d.VideoLink,
                    ImageLink = d.ImageLink
                })
                .ToList();

            var kursDetay = new
            {
                KursAdi = kurs.KursAdi,
                KursAciklama = kurs.KursAciklama,
                DersSayisi = kurs.DersSayisi,
                Fiyat = kurs.Fiyat,
                EgitmenBilgi = new
                {
                    Isim = kurs.Egitmen.Isim,
                    Soyisim = kurs.Egitmen.Soyisim,
                    KullaniciAdi = kurs.Egitmen.KullaniciAdi,
                    KisaBilgi = kurs.Egitmen.KisaBilgi
                },
                Icerikler = ilgiliKursDetaylari
            };

            return Json(kursDetay);
        }

        [HttpPost]
        public async Task<IActionResult> SatinAl(int kursId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Lütfen giriş yapınız." });
            }

            var username = User.Identity.Name;
            var kursiyerId = await kursiyerManager.GetKursiyerIDByKursiyerName(username);

            if (kursiyerId <= 0)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            var kursiyerKurslar = await kursiyerKursDetailManager.GetListWithIncludesAsync();
            var kursiyerKurs = kursiyerKurslar.Where(p => p.KursID == kursId && p.KursiyerID == kursiyerId).ToList();

            if (kursiyerKurs.Count() >= 1)
            {
                return Json(new { success = false, message = "Bu kursa sahipsiniz." });
            }
            else
            {

                var yeniKursiyerKursDetail = new KursiyerKursDetail
                {
                    KursID = kursId,
                    KursiyerID = kursiyerId,
                    Durum = (KursiyerKursDetailDurum)1
                };

                await kursiyerKursDetailManager.Add(yeniKursiyerKursDetail);

                var kurs = await kursManager.GetByID(kursId);
                kurs.DersSayisi += 1;
                await kursManager.Update(kurs);

                return Json(new { success = true, message = "Satın alma işlemi başarılı!" });
            }
        }



    }
}
