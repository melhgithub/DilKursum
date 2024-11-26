using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DilKursum.DataTransferObjects;
using DilKursum.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DilKursum.Controllers
{
    [Authorize(Policy = "admin")]
    public class KursDetailEditController : Controller
    {
        DilManager dilManager = new DilManager(new EFDilRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        public async Task<IActionResult> Index()
        {
            var diller = await dilManager.GetList();
            var egitmenler = await egitmenManager.GetList();
            var kursdetaylari = await kursDetailManager.GetList();
            var kurslar = await kursManager.GetList();
            var dto = new KursDetailDto
            {
                Kurslar = await kursManager.GetKursNames(),
                Diller = await dilManager.GetDilNames(),
                Egitmenler = await egitmenManager.GetEgitmenUserNames()
            };
            var model = new KursDetailModel
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
        public async Task<IActionResult> FilterKursDetail(KursDetailDto kursDetail)
        {
            var kursdetaylari = await kursDetailManager.GetListWithIncludesAsync();
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var egitmenler = await egitmenManager.GetEgitmenUserNames();
            var diller = await dilManager.GetDilNames();

            if (!string.IsNullOrEmpty(kursDetail.VideoLink))
            {
                var videolinkLower = kursDetail.VideoLink.ToLower();
                kursdetaylari = kursdetaylari.Where(k => k.VideoLink.ToLower().Contains(videolinkLower)).ToList();
            }
            if (!string.IsNullOrEmpty(kursDetail.ImageLink))
            {
                var imagelinkLower = kursDetail.ImageLink.ToLower();
                kursdetaylari = kursdetaylari.Where(k => k.ImageLink.ToLower().Contains(imagelinkLower)).ToList();
            }
            if (!string.IsNullOrEmpty(kursDetail.Description))
            {
                var descriptionLower = kursDetail.Description.ToLower();
                kursdetaylari = kursdetaylari.Where(k => k.Description.ToLower().Contains(descriptionLower)).ToList();
            }

            if (kursDetail.Durum != 0)
            {
                kursdetaylari = kursdetaylari.Where(d => d.Durum == kursDetail.Durum).ToList();
            }

            if (!string.IsNullOrEmpty(kursDetail.KursAdi))
            {
                kursdetaylari = kursdetaylari.Where(d => d.Kurs.ID.ToString() == kursDetail.KursAdi).ToList();
            }

            if (!string.IsNullOrEmpty(kursDetail.EgitmenKullaniciAdi))
            {
                kursdetaylari = kursdetaylari.Where(d => d.Kurs.EgitmenID.ToString() == kursDetail.EgitmenKullaniciAdi).ToList();
            }

            if (!string.IsNullOrEmpty(kursDetail.DilAdi))
            {
                kursdetaylari = kursdetaylari.Where(d => d.Kurs.DilID.ToString() == kursDetail.DilAdi).ToList();
            }


            var kursDetailData = kursdetaylari.Select(p => new
            {
                ID = p.ID,
                Videolink = p.VideoLink,
                Imagelink = p.ImageLink,
                Description = p.Description,
                Durum = p.Durum,
                Kurslar = kurslar,
                Kurs = new { ID = p.Kurs.ID, Name = p.Kurs.KursAdi },
                Dil = new { ID = p.Kurs.Dil?.ID, DilAdi = p.Kurs.Dil?.DilAdi },
                Egitmen = new { ID = p.Kurs.Egitmen?.ID, Isim = p.Kurs.Egitmen?.Isim, Kullaniciadi = p.Kurs.Egitmen?.KullaniciAdi }
            }).ToList();


            return Json(kursDetailData);
        }


        [HttpPost]
        public async Task<IActionResult> Filter()
        {
            var kursdetaylari = await kursDetailManager.GetListWithIncludesAsync();
            var kurslar = await kursManager.GetListWithIncludesAsync();
            var egitmenler = await egitmenManager.GetEgitmenUserNames();
            var diller = await dilManager.GetDilNames();

            var kursDetailData = kursdetaylari.Select(p => new
            {
                ID = p.ID,
                Videolink = p.VideoLink,
                Imagelink = p.ImageLink,
                Description = p.Description,
                Durum = p.Durum,
                Kurslar = kurslar,
                Kurs = new { ID = p.Kurs.ID, Name = p.Kurs.KursAdi },
                Dil = new { ID = p.Kurs.Dil?.ID, DilAdi = p.Kurs.Dil?.DilAdi },
                Egitmen = new { ID = p.Kurs.Egitmen?.ID, Isim = p.Kurs.Egitmen?.Isim, Kullaniciadi = p.Kurs.Egitmen?.KullaniciAdi }
            }).ToList();


            return Json(kursDetailData);
        }



        [HttpPost]
        public async Task<IActionResult> AddEditKursDetail(Dictionary<string, string> form, KursDetailAddDto kursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursDetail != null)
            {
                if (kursDetail.KursID == null)
                {
                    return Json(new { success = false, message = "Hata oluştu!" });
                }

                var kursDetailEntity = new KursDetail
                {
                    VideoLink = string.IsNullOrEmpty(kursDetail.VideoLink) ? null : kursDetail.VideoLink,
                    ImageLink = string.IsNullOrEmpty(kursDetail.ImageLink) ? null : kursDetail.ImageLink,
                    Description = string.IsNullOrEmpty(kursDetail.Description) ? null : kursDetail.Description,
                    KursID = kursDetail.KursID,
                    Durum = kursDetail.Durum,
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
                            var kurs = await kursManager.GetByID(detail.KursID);
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

        [HttpGet]
        public async Task<IActionResult> GetActiveKurslar()
        {
            var kurslar = await kursManager.GetListWithIncludesAsync();

            var activeKurslar = kurslar.Where(k => k.Durum == (KursDurum)1 && k.Dil.Durum == (DilDurum)1 && k.Egitmen.Durum == (EgitmenDurum)1).Select(k => new
            {
                ID = k.ID,
                KursAdi = k.KursAdi,
                EgitmenAdi = k.Egitmen.KullaniciAdi
            }).ToList();

            return Json(new { kurslar = activeKurslar });
        }


        [HttpPost]
        public async Task<IActionResult> ApproveKursDetail(KursDetailDto kursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursDetail != null)
            {
                try
                {
                    var kursDetailToApprove = await kursDetailManager.GetByID(kursDetail.ID);

                    if (kursDetailToApprove != null)
                    {
                        if (kursDetailToApprove.Durum == (KursDetailDurum)2)
                        {
                            var kurs = await kursManager.GetByID(kursDetailToApprove.KursID);
                            kurs.DersSayisi++;
                            await kursManager.Update(kurs);
                        }
                        kursDetailToApprove.Durum = (KursDetailDurum)1;
                        await kursDetailManager.Update(kursDetailToApprove);




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
        public async Task<IActionResult> RemoveKursDetail(KursDetailDto kursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursDetail != null)
            {
                try
                {
                    var kursDetailToDelete = await kursDetailManager.GetByID(kursDetail.ID);

                    var kurs = await kursManager.GetByID(kursDetailToDelete.KursID);
                    if (kursDetailToDelete != null)
                    {
                        if (kursDetailToDelete.Durum == (KursDetailDurum)1)
                        {
                            kurs.DersSayisi--;
                            await kursManager.Update(kurs);
                        }
                        kursDetailToDelete.Durum = (KursDetailDurum)2;
                        await kursDetailManager.Update(kursDetailToDelete);




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
        public async Task<IActionResult> DeleteKursDetail(KursDetailDto kursDetail)
        {
            var response = new { success = false, message = "" };

            if (kursDetail != null)
            {
                try
                {
                    var kursDetailToDelete = await kursDetailManager.GetByID(kursDetail.ID);

                    if (kursDetailToDelete != null)
                    {
                        await kursDetailManager.Delete(kursDetailToDelete);


                        var kurs = await kursManager.GetByID(kursDetailToDelete.KursID);
                        kurs.DersSayisi--;
                        await kursManager.Update(kurs);


                        response = new { success = true, message = "Kurs içeriği silindi" };
                    }
                    else
                    {
                        response = new { success = false, message = "Silinecek Kurs içeriği Bulunamadı" };
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
