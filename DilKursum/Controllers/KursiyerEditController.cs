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
    public class KursiyerEditController : Controller
    {
        KursiyerManager kursiyerManager = new KursiyerManager(new EFKursiyerRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());
        public async Task<IActionResult> Index()
        {
            var kursiyerler = await kursiyerManager.GetList();
            var dto = new KursiyerDto();
            var model = new KursiyerModel
            {
                Kursiyerler = kursiyerler,
                DTO = dto
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> GetKurslar(int kursiyerId)
        {
            var kursdetails = await kursiyerKursDetailManager.GetListWithIncludesAsync();

            var data = kursdetails.Where(x => x.KursiyerID == kursiyerId).ToList();

            return Json(data);
        }



        [HttpPost]
        public async Task<IActionResult> FilterKursiyer(KursiyerDto kursiyer)
        {
            var kursiyerler = await kursiyerManager.GetList();

            if (!string.IsNullOrEmpty(kursiyer.Isim))
            {
                var kursiyerIsimLower = kursiyer.Isim.ToLower();
                kursiyerler = kursiyerler.Where(k => k.Isim.ToLower().Contains(kursiyerIsimLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kursiyer.Soyisim))
            {
                var kursiyerSoyIsimLower = kursiyer.Soyisim.ToLower();
                kursiyerler = kursiyerler.Where(k => k.Soyisim.ToLower().Contains(kursiyerSoyIsimLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kursiyer.KullaniciAdi))
            {
                var kursiyerKullaniciAdiLower = kursiyer.KullaniciAdi.ToLower();
                kursiyerler = kursiyerler.Where(k => k.KullaniciAdi.ToLower().Contains(kursiyerKullaniciAdiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kursiyer.Email))
            {
                var kursiyerEmailLower = kursiyer.Email.ToLower();
                kursiyerler = kursiyerler.Where(k => k.Email.ToLower().Contains(kursiyerEmailLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kursiyer.Sifre))
            {
                var kursiyerSifreLower = kursiyer.Sifre.ToLower();
                kursiyerler = kursiyerler.Where(k => k.Sifre.ToLower().Contains(kursiyerSifreLower)).ToList();
            }

            if (!string.IsNullOrEmpty(kursiyer.Bakiye.ToString()) && kursiyer.Bakiye.ToString() != "0")
            {
                var bakiyeStr = kursiyer.Bakiye.ToString();
                kursiyerler = kursiyerler.Where(k => k.Bakiye.ToString().Contains(bakiyeStr)).ToList();
            }

            if (kursiyer.Durum != 0)
            {
                kursiyerler = kursiyerler.Where(d => d.Durum == kursiyer.Durum).ToList();
            }

            var kursiyerData = kursiyerler.Select(p => new
            {
                ID = p.ID,
                Email = p.Email,
                Kullaniciadi = p.KullaniciAdi,
                Isim = p.Isim,
                Soyisim = p.Soyisim,
                Sifre = p.Sifre,
                Bakiye = p.Bakiye,
                Durum = p.Durum,
            });

            return Json(kursiyerData);
        }


        [HttpPost]
        public async Task<IActionResult> Filter()
        {
            var kursiyerler = await kursiyerManager.GetList();

            var kursiyerData = kursiyerler.Select(p => new
            {
                ID = p.ID,
                Email = p.Email,
                Kullaniciadi = p.KullaniciAdi,
                Isim = p.Isim,
                Soyisim = p.Soyisim,
                Sifre = p.Sifre,
                Bakiye = p.Bakiye,
                Durum = p.Durum,
            });

            return Json(kursiyerData);
        }




        [HttpPost]
        public async Task<IActionResult> AddEditKursiyer(Dictionary<string, string> form, KursiyerAddDto kursiyer)
        {
            var response = new { success = false, message = "" };

            if (kursiyer != null)
            {
                bool kursiyerExists = await kursiyerManager.CheckKursiyerExists(kursiyer.KullaniciAdi, kursiyer.Email);

                if (kursiyerExists)
                {
                    return Json(response = new { success = false, message = "Lütfen formu kontrol ediniz." });
                }

                try
                {
                    if (decimal.TryParse(form[$"Bakiye"], out decimal bakiye))
                    {
                        var entity = kursiyer.ID > 0 ? await kursiyerManager.GetByID(kursiyer.ID) : new Kursiyer();

                        entity.Email = kursiyer.Email;
                        entity.KullaniciAdi = kursiyer.KullaniciAdi;
                        entity.Isim = kursiyer.Isim;
                        entity.Soyisim = kursiyer.Soyisim;
                        entity.Sifre = kursiyer.Sifre;
                        entity.Bakiye = bakiye;
                        entity.Durum = kursiyer.Durum;


                        if (entity.ID > 0)
                        {
                            await kursiyerManager.Update(entity);
                            response = new { success = true, message = "Kursiyer başarıyla güncellendi!" };
                        }
                        else
                        {
                            var kursiyerKadiSorgu = await kursiyerManager.GetKursiyerIDByKursiyerName(entity.KullaniciAdi);
                            var kursiyerKadiS = await kursiyerManager.GetByID(kursiyerKadiSorgu);
                            if (kursiyerKadiSorgu != null && kursiyerKadiSorgu > 0)
                            {
                                response = new { success = true, message = "Kullanıcı Adını Kontrol Ediniz!" };
                            }
                            else
                            {
                                await kursiyerManager.Add(entity);
                                response = new { success = true, message = "Kursiyer başarıyla eklendi!" };
                            }
                        }
                    }

                    else
                    {
                        return Json(response = new { success = false, message = "Lütfen formu kontrol ediniz." });
                    }

                }
                catch (Exception)
                {
                    response = new { success = false, message = "Lütfen formu kontrol ediniz." };
                }
            }
            else
            {
                response = new { success = false, message = "Hata oluştu!" };
            }

            return Json(response);
        }



        [HttpPost]
        public async Task<IActionResult> ApproveKursiyer(KursiyerDto kursiyer)
        {
            var response = new { success = false, message = "" };

            if (kursiyer != null)
            {
                try
                {
                    var kursiyerToApprove = await kursiyerManager.GetByID(kursiyer.ID);

                    if (kursiyerToApprove != null)
                    {
                        kursiyerToApprove.Durum = (KursiyerDurum)1;
                        kursiyerManager.Update(kursiyerToApprove);

                        response = new { success = true, message = "Kursiyer Onaylandı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Onaylanacak Kursiyer Bulunamadı" };
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
        public async Task<IActionResult> RemoveKursiyer(KursiyerDto kursiyer)
        {
            var response = new { success = false, message = "" };

            if (kursiyer != null)
            {
                try
                {
                    var kursiyerToDelete = await kursiyerManager.GetByID(kursiyer.ID);

                    if (kursiyerToDelete != null)
                    {
                        kursiyerToDelete.Durum = (KursiyerDurum)2;
                        kursiyerManager.Update(kursiyerToDelete);

                        response = new { success = true, message = "Kursiyer Kaldırıldı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Kaldırılacak Kursiyer Bulunamadı" };
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
        public async Task<IActionResult> DeleteKursiyer(KursiyerDto kursiyer)
        {
            var response = new { success = false, message = "" };

            if (kursiyer == null)
            {
                response = new { success = false, message = "Hata Oluştu" };
                return Json(response);
            }

            try
            {
                var kursiyerToDelete = await kursiyerManager.GetByID(kursiyer.ID);
                if (kursiyerToDelete == null)
                {
                    response = new { success = false, message = "Silinecek Kursiyer Bulunamadı" };
                    return Json(response);
                }
                var kursiyerkursdetails = (await kursiyerKursDetailManager.GetList()).Where(kkd => kkd.KursiyerID == kursiyerToDelete.ID).ToList();

                foreach (var kursiyerkursdetail in kursiyerkursdetails)
                {
                    await kursiyerKursDetailManager.Delete(kursiyerkursdetail);
                }

                await kursiyerManager.Delete(kursiyerToDelete);

                response = new { success = true, message = "Kursiyer Silindi" };
            }
            catch (Exception ex)
            {
                response = new { success = false, message = $"Bilgileri Kontrol Ediniz: {ex.Message}" };
            }

            return Json(response);
        }


    }
}
