using BusinessLayer.Concrete;
using CoreLayer.Extensions;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class KursEditController : Controller
    {
        DilManager dilManager = new DilManager(new EFDilRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());

        public async Task<IActionResult> Index()
        {
            var diller = await dilManager.GetList();
            var egitmenler = await egitmenManager.GetList();
            var kurslar = await kursManager.GetList();
            var kursdetaylari = await kursDetailManager.GetList();
            var dto = new KursDto
            {
                Diller = await dilManager.GetDilNames(),
                Egitmenler = await egitmenManager.GetEgitmenUserNames()
            };
            var model = new KursModel
            {
                Diller = diller,
                Egitmenler = egitmenler,
                Kurslar = kurslar,
                KursDetaylari = kursdetaylari,
                DTO = dto
            };
            return View(model);
        }

        [HttpPost]
        [Route("KursEdit/FilterKurs")]
        public async Task<IActionResult> FilterKurs(KursDto kurs)
        {
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var egitmenler = await egitmenManager.GetEgitmenUserNames();
            var diller = await dilManager.GetDilNames();


            if (!string.IsNullOrEmpty(kurs.KursAdi))
            {
                var kursAdiLower = kurs.KursAdi.ToLower();
                kurslar = kurslar.Where(k => k.KursAdi.ToLower().Contains(kursAdiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kurs.KursAciklama))
            {
                var kursAciklamaLower = kurs.KursAciklama.ToLower();
                kurslar = kurslar.Where(k => k.KursAciklama.ToLower().Contains(kursAciklamaLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kurs.DersSayisi.ToString()) && kurs.DersSayisi.ToString() != "0")
            {
                var dersSayiStr = kurs.DersSayisi.ToString();
                kurslar = kurslar.Where(k => k.DersSayisi.ToString().Contains(dersSayiStr)).ToList();
            }

            if (!string.IsNullOrEmpty(kurs.Fiyat.ToString()) && kurs.Fiyat.ToString() != "0")
            {
                var fiyatStr = kurs.Fiyat.ToString();
                kurslar = kurslar.Where(k => k.Fiyat.ToString().Contains(fiyatStr)).ToList();
            }

            if (kurs.Durum != 0)
            {
                kurslar = kurslar.Where(d => d.Durum == kurs.Durum).ToList();
            }

            if (!string.IsNullOrEmpty(kurs.EgitmenKullaniciAdi))
            {
                kurslar = kurslar.Where(d => d.EgitmenID.ToString() == kurs.EgitmenKullaniciAdi).ToList();
            }

            if (!string.IsNullOrEmpty(kurs.DilAdi))
            {
                kurslar = kurslar.Where(d => d.DilID.ToString() == kurs.DilAdi).ToList();
            }



            var kursData = kurslar.Select(p => new
            {
                ID = p.ID,
                Derssayi = p.DersSayisi,
                Kursadi = p.KursAdi,
                Kursaciklama = p.KursAciklama,
                Fiyat = p.Fiyat,
                Durum = p.Durum,
                Egitmenler = egitmenler,
                Diller = diller,
                Egitmen = new { p.Egitmen.ID, Name = p.Egitmen.KullaniciAdi },
                Dil = new { p.Dil.ID, diladi = p.Dil.DilAdi }
            });

            return Json(kursData);
        }


        [HttpPost]
        public async Task<IActionResult> Filter()
        {
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var egitmenler = await egitmenManager.GetEgitmenUserNames();
            var diller = await dilManager.GetDilNames();

            var kursData = kurslar.Select(p => new
            {
                ID = p.ID,
                Derssayi = p.DersSayisi,
                Kursadi = p.KursAdi,
                Kursaciklama = p.KursAciklama,
                Fiyat = p.Fiyat,
                Durum = p.Durum,
                Egitmenler = egitmenler,
                Diller = diller,
                Egitmen = new { p.Egitmen.ID, Name = p.Egitmen.KullaniciAdi },
                Dil = new {p.Dil.ID, diladi = p.Dil.DilAdi }
            });

            return Json(kursData);
        }


        public async Task<JsonResult> GetActiveEgitmenlerAndDiller()
        {
            var d1egitmenler = await egitmenManager.GetList();
            var d1diller = await dilManager.GetList();
            var egitmenler = d1egitmenler.Where(e => e.Durum == (EgitmenDurum)1).ToList();
            var diller = d1diller.Where(d => d.Durum == (DilDurum)1).ToList();

            return Json(new
            {
                egitmenler = egitmenler.Select(e => new { e.ID, e.KullaniciAdi }).ToList(),
                diller = diller.Select(d => new { d.ID, d.DilAdi }).ToList()
            });
        }


        [HttpPost]
        public async Task<IActionResult> GetKurslar(int kursId)
        {
            var kursdetails = await kursDetailManager.GetList();

            var data = kursdetails.Where(x => x.KursID == kursId).ToList();

            return Json(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetKursDetailsByKursID(int kursId)
        {
            var kursdetails = await kursDetailManager.GetList();
            var data = kursdetails.Where(x => x.KursID == kursId).ToList();

            return Json(data);
        }


        [HttpPost]
        public async Task<IActionResult> AddEditKurs(Dictionary<string, string> form, KursAddDto kurs)
        {
            var response = new { success = false, message = "" };

            if (kurs != null)
            {
                try
                {
                    if(decimal.TryParse(form[$"Fiyat"], out decimal kursFiyat))
                    {
                        var kursEntity = new Kurs
                        {
                            KursAciklama = kurs.KursAciklama,
                            KursAdi = kurs.KursAdi,
                            Fiyat = kursFiyat,

                            EgitmenID = await egitmenManager.GetEgitmenIdByUserName(kurs.EgitmenKullaniciAdi),
                            DilID = await dilManager.GetDilIdByDilAdi(kurs.DilAdi),
                            Durum = kurs.Durum,
                        };

                        if (kurs.ID > 0)
                        {
                            kursEntity.ID = kurs.ID;
                            await kursManager.Update(kursEntity);
                        }
                        else
                        {
                            await kursManager.Add(kursEntity);
                        }


                        response = new { success = true, message = "Kurs başarıyla kaydedildi!" };
                    }
                    else
                    {
                        return Json("Bir sorun oluştu! Lütfen bilgileri kontrol ediniz.");
                    }

                }
                catch (Exception)
                {
                    response = new { success = false, message = "Kurs eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz." };
                }

            }
            else
            {
                response = new { success = false, message = "Hata oluştu!" };
            }

            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveKurs(KursDto kurs)
        {
            var response = new { success = false, message = "" };

            if (kurs != null)
            {
                try
                {
                    var kursToApprove = await kursManager.GetByID(kurs.ID);

                    if (kursToApprove != null)
                    {
                        kursToApprove.Durum = (KursDurum)1;
                        kursManager.Update(kursToApprove);

                        response = new { success = true, message = "Kurs Onaylandı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Onaylanacak Kurs Bulunamadı" };
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
        public async Task<IActionResult> RemoveKurs(KursDto kurs)
        {
            var response = new { success = false, message = "" };

            if (kurs != null)
            {
                try
                {
                    var kursToDelete = await kursManager.GetByID(kurs.ID);

                    if (kursToDelete != null)
                    {
                        var kursdetails = await kursDetailManager.GetList();
                        var kursDetailsToDelete = kursdetails.Where(p=>p.KursID==kurs.ID).ToList();
                        foreach(var kursdetailtodelete in kursDetailsToDelete)
                        {
                            kursdetailtodelete.Durum = (KursDetailDurum)2;
                            await kursDetailManager.Update(kursdetailtodelete);
                        };
                        kursToDelete.Durum = (KursDurum)2;
                        await kursManager.Update(kursToDelete);

                        response = new { success = true, message = "Kurs Kaldırıldı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Kaldırılacak Kurs Bulunamadı" };
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
        public async Task<IActionResult> DeleteKurs(KursDto kurs)
        {
            var response = new { success = false, message = "" };

            if (kurs == null)
            {
                response = new { success = false, message = "Hata Oluştu" };
                return Json(response);
            }

            try
            {
                var kursToDelete = await kursManager.GetByID(kurs.ID);
                if (kursToDelete == null)
                {
                    response = new { success = false, message = "Silinecek Kurs Bulunamadı" };
                    return Json(response);
                }

                var kursdetails = (await kursDetailManager.GetList()).Where(kd => kd.KursID == kursToDelete.ID).ToList();
                var kursiyerkursdetails = (await kursiyerKursDetailManager.GetList()).Where(kkd => kkd.KursID == kursToDelete.ID).ToList();

                foreach (var kursiyerkursdetail in kursiyerkursdetails)
                {
                    await kursiyerKursDetailManager.Delete(kursiyerkursdetail);
                }

                foreach (var kursdetail in kursdetails)
                {
                    await kursDetailManager.Delete(kursdetail);
                }

                await kursManager.Delete(kursToDelete);

                response = new { success = true, message = "Kurs Silindi" };
            }
            catch (Exception ex)
            {
                response = new { success = false, message = $"Bilgileri Kontrol Ediniz: {ex.Message}" };
            }

            return Json(response);
        }


    }
}
