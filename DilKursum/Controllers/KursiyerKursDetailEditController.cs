using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class KursiyerKursDetailEditController : Controller
    {
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        DilManager dilManager = new DilManager(new EFDilRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        public async Task<IActionResult> Index()
        {
            var kursiyerler = await kursiyerManager.GetList();
            var kursiyerkursdetails = await kursiyerKursDetailManager.GetList();
            var kurslar = await kursManager.GetList();
            var diller = await dilManager.GetList();
            var kursdetails = await kursDetailManager.GetList();
            var egitmenler = await egitmenManager.GetList();

            var DTO = new KursiyerKursDetailsDto
            {
                Kursiyerler=await kursiyerManager.GetKursiyerUserNames(),
                Diller = await dilManager.GetDilNames(),
                Egitmenler = await egitmenManager.GetEgitmenUserNames(),
                Kurslar = await kursManager.GetKursNames(),
            };
            var model = new KursiyerKursDetailsModel
            {
                Kurslar = kurslar,
                Egitmenler = egitmenler,
                Diller = diller,
                Kursiyerler = kursiyerler,
                KursDetaylari = kursdetails,
                KursiyerKursDetaylari = kursiyerkursdetails,
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> FilterKursiyerKursDetail(KursiyerKursDetailsDto kursiyerKursDetails)
        {
            var kursdetaylari = await kursDetailManager.GetListWithIncludesAsync();
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var egitmenler = await egitmenManager.GetEgitmenUserNames();
            var kursiyerler = await kursiyerManager.GetList();
            var kursiyerkursdetaylari = await kursiyerKursDetailManager.GetListWithIncludesAsync();
            var diller = await dilManager.GetDilNames();



            if (kursiyerKursDetails.Durum != 0)
            {
                kursiyerkursdetaylari = kursiyerkursdetaylari.Where(d => d.Durum == kursiyerKursDetails.Durum).ToList();
            }

            if (kursiyerKursDetails.KursiyerID > 0)
            {
                kursiyerkursdetaylari = kursiyerkursdetaylari.Where(d => d.KursiyerID == kursiyerKursDetails.KursiyerID).ToList();
            }

            if (kursiyerKursDetails.KursID > 0)
            {
                kursiyerkursdetaylari = kursiyerkursdetaylari.Where(d => d.Kurs.ID == kursiyerKursDetails.KursID).ToList();
            }

            if (kursiyerKursDetails.EgitmenID > 0)
            {
                kursiyerkursdetaylari = kursiyerkursdetaylari.Where(d => d.Kurs.EgitmenID == kursiyerKursDetails.EgitmenID).ToList();
            }

            if (kursiyerKursDetails.DilID > 0)
            {
                kursiyerkursdetaylari = kursiyerkursdetaylari.Where(d => d.Kurs.DilID == kursiyerKursDetails.DilID).ToList();
            }

            var kursiyerKursDetailData = kursiyerkursdetaylari.Select(p => new
            {
                ID = p.ID,
                Durum = p.Durum,
                Kurslar = kurslar,
                Kursiyerler = kursiyerler,
                Kursiyerkursdetaylari = kursiyerkursdetaylari,
                Kursiyer = new { ID = p.KursiyerID, Name = p.Kursiyer.KullaniciAdi },
                Kurs = new { ID = p.Kurs.ID, Name = p.Kurs.KursAdi },
                Dil = new { ID = p.Kurs.Dil?.ID, DilAdi = p.Kurs.Dil?.DilAdi },
                Egitmen = new { ID = p.Kurs.Egitmen?.ID, Name = p.Kurs.Egitmen?.KullaniciAdi }
            }).ToList();


            return Json(kursiyerKursDetailData);
        }
        [HttpPost]
        public async Task<IActionResult> Filter()
        {
            var kursdetaylari = await kursDetailManager.GetListWithIncludesAsync();
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var egitmenler = await egitmenManager.GetEgitmenUserNames();
            var kursiyerler = await kursiyerManager.GetList();
            var kursiyerkursdetaylari = await kursiyerKursDetailManager.GetListWithIncludesAsync();
            var diller = await dilManager.GetDilNames();

            var kursiyerKursDetailData = kursiyerkursdetaylari.Select(p => new
            {
                ID = p.ID,
                Durum = p.Durum,
                Kurslar = kurslar,
                Kursiyerler = kursiyerler,
                Kursiyerkursdetaylari = kursiyerkursdetaylari,
                Kursiyer = new { ID = p.KursiyerID, Name = p.Kursiyer.KullaniciAdi },
                Kurs = new { ID = p.Kurs.ID, Name = p.Kurs.KursAdi },
                Dil = new { ID = p.Kurs.Dil?.ID, DilAdi = p.Kurs.Dil?.DilAdi },
                Egitmen = new { ID = p.Kurs.Egitmen?.ID, Name = p.Kurs.Egitmen?.KullaniciAdi }
            }).ToList();


            return Json(kursiyerKursDetailData);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveKurslarAndActiveKursiyerler()
        {
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var kursiyerler = await kursiyerManager.GetList();

            var activeKurslar = kurslar.Where(k => k.Durum == (KursDurum)1).Select(k => new
            {
                ID = k.ID,
                KursAdi = k.KursAdi,
                EgitmenAdi = k.Egitmen.KullaniciAdi
            }).ToList();

            var activeKursiyerler = kursiyerler.Where(p => p.Durum == (KursiyerDurum)1).Select(p => new
            {
                ID = p.ID,
                KullaniciAdi = p.KullaniciAdi,
            }).ToList();

            return Json(new { kurslar = activeKurslar, kursiyerler = activeKursiyerler });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditKursiyerKursDetail(Dictionary<string, string> form, KursiyerKursDetailsAddDto kursiyerKursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursiyerKursDetail != null)
            {
                try
                {
                    if (kursiyerKursDetail.KursID == null || kursiyerKursDetail.KursiyerKullaniciAdi == null || kursiyerKursDetail.Durum == null)
                    {
                        response = new { success = false, message = "Hata oluştu!" };

                        return Json(response);
                    }
                    else
                    {

                        var kursDetailEntity = new KursiyerKursDetail
                        {
                            KursID = kursiyerKursDetail.KursID,
                            KursiyerID = await kursiyerManager.GetKursiyerIDByKursiyerName(kursiyerKursDetail.KursiyerKullaniciAdi),
                            Durum = kursiyerKursDetail.Durum,
                        };

                        if (kursiyerKursDetail.ID > 0)
                        {
                            kursDetailEntity.ID = kursiyerKursDetail.ID;
                            await kursiyerKursDetailManager.Update(kursDetailEntity);
                        }
                        else
                        {
                            await kursiyerKursDetailManager.Add(kursDetailEntity);
                        }
                    }

                    response = new { success = true, message = "Kurs içeriği başarıyla kaydedildi!" };
                }
                catch (Exception)
                {
                    response = new { success = false, message = "Kurs içeriği eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz." };
                }

            }
            else
            {
                response = new { success = false, message = "Hata oluştu!" };
            }

            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveKursiyerKursDetail(KursiyerKursDetailsDto kursiyerKursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursiyerKursDetail != null)
            {
                try
                {
                    var kursiyerKursDetailToApprove = await kursiyerKursDetailManager.GetByID(kursiyerKursDetail.ID);

                    if (kursiyerKursDetailToApprove != null)
                    {
                        kursiyerKursDetailToApprove.Durum = (KursiyerKursDetailDurum)1;
                        kursiyerKursDetailManager.Update(kursiyerKursDetailToApprove);

                        response = new { success = true, message = "Kurs içeriği Onaylandı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Onaylanacak Kurs içeriği Bulunamadı" };
                    }
                }
                catch (Exception)
                {
                    response = new { success = false, message = "Bilgileri Kontrol Ediniz" };
                }
            }
            else
            {
                response = new { success = false, message = "Hata Oluştu" };
            }

            return Json(response);
        }



        [HttpPost]
        public async Task<IActionResult> RemoveKursiyerKursDetail(KursiyerKursDetailsDto kursiyerKursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursiyerKursDetail != null)
            {
                try
                {
                    var kursiyerKursDetailToDelete = await kursiyerKursDetailManager.GetByID(kursiyerKursDetail.ID);

                    if (kursiyerKursDetailToDelete != null)
                    {
                        kursiyerKursDetailToDelete.Durum = (KursiyerKursDetailDurum)2;
                        kursiyerKursDetailManager.Update(kursiyerKursDetailToDelete);

                        response = new { success = true, message = "Kurs içeriği Kaldırıldı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Kaldırılacak Kurs içeriği Bulunamadı" };
                    }
                }
                catch (Exception)
                {
                    response = new { success = false, message = "Bilgileri Kontrol Ediniz" };
                }
            }
            else
            {
                response = new { success = false, message = "Hata Oluştu" };
            }

            return Json(response);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteKursiyerKursDetail(KursiyerKursDetailsDto kursiyerKursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursiyerKursDetail != null)
            {
                try
                {
                    var kursiyerKursDetailToDelete = await kursiyerKursDetailManager.GetByID(kursiyerKursDetail.ID);

                    if (kursiyerKursDetailToDelete != null)
                    {
                        await kursiyerKursDetailManager.Delete(kursiyerKursDetailToDelete);



                        response = new { success = true, message = "Kursiyer kursu silindi" };
                    }
                    else
                    {
                        response = new { success = false, message = "Silinecek Kursiyer kursu Bulunamadı" };
                    }
                }
                catch (Exception)
                {
                    response = new { success = false, message = "Bilgileri Kontrol Ediniz" };
                }
            }
            else
            {
                response = new { success = false, message = "Hata Oluştu" };
            }

            return Json(response);
        }


    }
}
