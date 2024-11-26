using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrete;
using DilKursum.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using DilKursum.DataTransferObjects;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Azure;
using BusinessLayer.Abstract;

namespace DilKursum.Controllers
{
    [Authorize]
    public class KurslarimController : Controller
    {
        DilManager dilManager = new DilManager(new EFDilRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());
        KursFileManager kursFileManager = new KursFileManager(new EFKursFileRepository());


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
                var alldiller = await dilManager.GetList();
                var diller = alldiller.Where(p => p.Durum==(DilDurum)1).ToList();
                var egitmenId = await egitmenManager.GetEgitmenIdByUserName(username);

                var allkurslar = await kursManager.GetListWithIncludesAsync();
                var kurslar = allkurslar.Where(p => p.EgitmenID == egitmenId && p.Dil.Durum == (DilDurum)1).ToList();

                var allfiles = await kursFileManager.GetListWithIncludesAsync();
                var files = allfiles.Where(p => p.EgitmenID == egitmenId).ToList();

                var allkursiyerKurslar = await kursiyerKursDetailManager.GetListWithIncludesAsync();

                var kursdetaylari = new List<KursDetail>();

                var kursiyerSayilari = new Dictionary<int, int>();

                foreach (var kurs in kurslar)
                {
                    var detaylar = await kursDetailManager.GetListByKursID(kurs.ID);
                    kursdetaylari.AddRange(detaylar);

                    var kursiyerSayisi = allkursiyerKurslar.Count(k => k.KursID == kurs.ID);
                    kursiyerSayilari[kurs.ID] = kursiyerSayisi;

                }



                var egitmenler = await egitmenManager.GetList();

                var model = new KurslarimModel
                {
                    Diller = diller,
                    Kurslar = kurslar,
                    KursDetaylari = kursdetaylari,
                    Egitmenler = egitmenler,
                    KursiyerSayilari = kursiyerSayilari,
                    Dosyalar = files
                };
                return View("Index", model);

            }
           
            else if (role == "kursiyer")
            {
                var diller = await dilManager.GetList();
                var kursiyerID = await kursiyerManager.GetKursiyerIDByKursiyerName(username);
                var kursiyer = await kursiyerManager.GetByID(kursiyerID);
                var egitmenler = await egitmenManager.GetList();

                var allkursiyerKursDetails = await kursiyerKursDetailManager.GetListByKursiyerID(kursiyer.ID);
                var kursiyerKursDetails = allkursiyerKursDetails.Where(p => p.Durum == (KursiyerKursDetailDurum)1).ToList();

                var kurslar = new List<Kurs>();
                var kursdetaylari = new List<KursDetail>();

                var files = new List<KursFile>();


                var allkurslar = await kursManager.GetListWithIncludesAsync();
                foreach (var kursiyerKursDetail in kursiyerKursDetails)
                {
                    var kurs = allkurslar.FirstOrDefault(k => k.ID == kursiyerKursDetail.KursID);
                    if (kurs != null)
                    {
                        kurslar.Add(kurs);
                        foreach(var file in files)
                        {
                            if (file.EgitmenID == kurs.EgitmenID)
                            {
                                files.Add(file);
                            }
                        }

                        var detaylar = await kursDetailManager.GetListByKursID(kurs.ID);
                        kursdetaylari.AddRange(detaylar);
                    }
                }

                var model = new KurslarimModel
                {
                    Diller = diller,
                    Kurslar = kurslar,
                    KursDetaylari = kursdetaylari,
                    Egitmenler = egitmenler,
                    Dosyalar = files
                };
                return View("Index", model);


            }

            return RedirectToAction("Index", "Home");
        }



        [Authorize(Policy = "egitmen")]
        public async Task<IActionResult> AddEditKurs(Dictionary<string, string> form, KursAddDto kurs)
        {

            var response = new { success = false, message = "" };

            if (kurs != null)
            {
                if (decimal.TryParse(form[$"Fiyat"], out decimal kursFiyat))
                {
                    var username = HttpContext.Session.GetString("username");

                    var egitmenID = await egitmenManager.GetEgitmenIdByUserName(username);

                    if (username != kurs.EgitmenKullaniciAdi)
                    {
                        await HttpContext.SignOutAsync();
                        HttpContext.Session.Clear();

                        return RedirectToAction("Index", "Home");
                    }

                    var entity = new Kurs
                    {
                        KursAdi = kurs.KursAdi,
                        KursAciklama = kurs.KursAciklama,
                        Fiyat = kursFiyat,
                        DersSayisi = 0,
                        EgitmenID = await egitmenManager.GetEgitmenIdByUserName(kurs.EgitmenKullaniciAdi),
                        DilID = await dilManager.GetDilIdByDilAdi(kurs.DilAdi),
                        Durum = (KursDurum)2,

                    };

                    if (kurs.ID > 0)
                    {
                        entity.ID = kurs.ID;
                        await kursManager.Update(entity);
                    }
                    else
                    {
                        await kursManager.Add(entity);
                    }

                    response = new { success = true, message = "Kurs başarıyla kaydedildi!" };
                }
                else
                {
                    return Json("Bir sorun oluştu! Lütfen bilgileri kontrol ediniz.");
                }
            }
            else
            {
                response = new { success = false, message = "Hata oluştu!" };
            }

            return Json(response);

        }



        [Authorize(Policy = "egitmen")]
        [HttpPost]
        public async Task<IActionResult> AddEditKursDetailForEgitmen(Dictionary<string, string> form, KursDetailAddDto kursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursDetail != null)
            {
                if (kursDetail.KursID == null)
                {
                    return Json(new { success = false, message = "Hata oluştu!" });
                }


                var kurs = await kursManager.GetByID(kursDetail.KursID);
                var username = HttpContext.Session.GetString("username");
                var egitmenId = await egitmenManager.GetEgitmenIdByUserName(username);

                if (kurs.EgitmenID != egitmenId)
                {
                    await HttpContext.SignOutAsync();
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index", "Home");
                }

                var kursDetailEntity = new KursDetail
                {
                    VideoLink = string.IsNullOrEmpty(kursDetail.VideoLink) ? null : kursDetail.VideoLink,
                    ImageLink = string.IsNullOrEmpty(kursDetail.ImageLink) ? null : kursDetail.ImageLink,
                    Description = string.IsNullOrEmpty(kursDetail.Description) ? null : kursDetail.Description,
                    KursID = kursDetail.KursID,
                    Durum = (KursDetailDurum)2,
                };

                try
                {
                    if (kursDetail.ID > 0)
                    {
                        var detail = await kursDetailManager.GetByID(kursDetail.ID);

                        if (detail == null)
                        {
                            return Json(new { success = false, message = "Güncellenecek içerik bulunamadı." });
                        }

                        if (kursDetailEntity.Durum != detail.Durum)
                        {
                            kurs = await kursManager.GetByID(detail.KursID);
                            if (kursDetailEntity.Durum == KursDetailDurum.Approved)
                            {
                                kurs.DersSayisi += 1;
                            }
                            else
                            {
                                if (kurs.DersSayisi >= 1)
                                {
                                    kurs.DersSayisi -= 1;
                                }
                            }

                            await kursManager.Update(kurs);
                        }

                        kursDetailEntity.ID = kursDetail.ID;
                        await kursDetailManager.Update(kursDetailEntity);
                    }
                    else
                    {
                        await kursDetailManager.Add(kursDetailEntity);

                        var updatedKurs = await kursManager.GetByID(kursDetailEntity.KursID);
                        if (kursDetailEntity.Durum == KursDetailDurum.Approved)
                        {
                            updatedKurs.DersSayisi++;
                        }
                        await kursManager.Update(updatedKurs);
                    }

                    response = new { success = true, message = "Başarılı!" };
                }
                catch
                {
                    response = new { success = false, message = "Hata oluştu!" };
                }
            }
            else
            {
                response = new { success = false, message = "Kurs detayları eksik!" };
            }

            return Json(response);
        }




    }
}