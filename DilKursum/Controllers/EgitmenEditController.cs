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
    public class EgitmenEditController : Controller
    {
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());
        public async Task<IActionResult> Index()
        {
            var egitmenler = await egitmenManager.GetList();
            var kurslar = await kursManager.GetList();
            var kursdetaylari = await kursDetailManager.GetList();
            var dto = new EgitmenDto();
            var model = new EgitmenModel
            {
                Egitmenler = egitmenler,
                Kurslar = kurslar,
                KursDetaylari = kursdetaylari,
                DTO = dto
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterEgitmen(EgitmenDto egitmen)
        {
            var egitmenler = await egitmenManager.GetList();

            if (!string.IsNullOrEmpty(egitmen.Isim))
            {
                var egitmenIsimLower = egitmen.Isim.ToLower();
                egitmenler = egitmenler.Where(k => k.Isim.ToLower().Contains(egitmenIsimLower)).ToList();
            }

            if (!string.IsNullOrEmpty(egitmen.Soyisim))
            {
                var egitmenSoyIsimLower = egitmen.Soyisim.ToLower();
                egitmenler = egitmenler.Where(k => k.Soyisim.ToLower().Contains(egitmenSoyIsimLower)).ToList();
            }

            if (!string.IsNullOrEmpty(egitmen.KullaniciAdi))
            {
                var egitmenKullaniciAdiLower = egitmen.KullaniciAdi.ToLower();
                egitmenler = egitmenler.Where(k => k.KullaniciAdi.ToLower().Contains(egitmenKullaniciAdiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(egitmen.Email))
            {
                var egitmenEmailLower = egitmen.Email.ToLower();
                egitmenler = egitmenler.Where(k => k.Email.ToLower().Contains(egitmenEmailLower)).ToList();
            }

            if (!string.IsNullOrEmpty(egitmen.Sifre))
            {
                var egitmenSifreLower = egitmen.Sifre.ToLower();
                egitmenler = egitmenler.Where(k => k.Sifre.ToLower().Contains(egitmenSifreLower)).ToList();
            }

            if (!string.IsNullOrEmpty(egitmen.KisaBilgi))
            {
                var egitmenKisaBilgiLower = egitmen.KisaBilgi.ToLower();
                egitmenler = egitmenler.Where(k => k.KisaBilgi.ToLower().Contains(egitmenKisaBilgiLower)).ToList();
            }

            if (!string.IsNullOrEmpty(egitmen.Bakiye.ToString()) && egitmen.Bakiye.ToString() != "0")
            {
                var bakiyeStr = egitmen.Bakiye.ToString();
                egitmenler = egitmenler.Where(k => k.Bakiye.ToString().Contains(bakiyeStr)).ToList();
            }

            if (egitmen.Durum != 0)
            {
                egitmenler = egitmenler.Where(d => d.Durum == egitmen.Durum).ToList();
            }

            var egitmenData = egitmenler.Select(p => new
            {
                ID = p.ID,
                Email = p.Email,
                Kullaniciadi = p.KullaniciAdi,
                Isim = p.Isim,
                Soyisim = p.Soyisim,
                Sifre = p.Sifre,
                KisaBilgi = p.KisaBilgi,
                Bakiye = p.Bakiye,
                Durum = p.Durum,
            });

            return Json(egitmenData);
        }




        [HttpPost]
        public async Task<IActionResult> Filter()
        {
            var egitmenler = await egitmenManager.GetList();

            var egitmenData = egitmenler.Select(p => new
            {
                ID = p.ID,
                Email = p.Email,
                Kullaniciadi = p.KullaniciAdi,
                Isim = p.Isim,
                Soyisim = p.Soyisim,
                Sifre = p.Sifre,
                KisaBilgi = p.KisaBilgi,
                Bakiye = p.Bakiye,
                Durum = p.Durum,
            });

            return Json(egitmenData);
        }



        [HttpPost]
        public async Task<IActionResult> GetKurslar(int egitmenId)
        {
            var kurslar = await kursManager.GetList();

            var egitmenKurslar = kurslar.Where(k => k.EgitmenID == egitmenId).ToList();


            return Json(egitmenKurslar);
        }


        [HttpPost]
        public async Task<IActionResult> AddEditEgitmen(Dictionary<string, string> form, EgitmenAddDto egitmen)
        {
            var response = new { success = false, message = "" };

            if (egitmen != null)
            {
                bool egitmenExists = await egitmenManager.CheckEgitmenExists(egitmen.KullaniciAdi, egitmen.Email);

                if (egitmenExists && egitmen.ID == 0)
                {
                    return Json(response = new { success = true, message = "Lütfen formu kontrol ediniz." });
                }

                try
                {
                    if (decimal.TryParse(form[$"Bakiye"], out decimal bakiye))
                    {
                        var entity = egitmen.ID > 0 ? await egitmenManager.GetByID(egitmen.ID) : new Egitmen();

                        entity.Email = egitmen.Email;
                        entity.KullaniciAdi = egitmen.KullaniciAdi;
                        entity.Isim = egitmen.Isim;
                        entity.Soyisim = egitmen.Soyisim;
                        entity.Sifre = egitmen.Sifre;
                        entity.KisaBilgi = egitmen.KisaBilgi;
                        entity.Bakiye = bakiye;
                        entity.Durum = egitmen.Durum;


                        if (entity.ID > 0)
                        {
                            await egitmenManager.Update(entity);
                            response = new { success = true, message = "Eğitmen başarıyla güncellendi!" };
                        }
                        else
                        {
                            var egitmenKadiSorgu = await egitmenManager.GetEgitmenIdByUserName(entity.KullaniciAdi);
                            var egitmenKadiS = await egitmenManager.GetByID(egitmenKadiSorgu);
                            if (egitmenKadiSorgu != null && egitmenKadiSorgu > 0)
                            {
                                response = new { success = false, message = "Kullanıcı Adını Kontrol Ediniz!" };
                            }
                            else
                            {
                                await egitmenManager.Add(entity);
                                response = new { success = true, message = "Eğitmen başarıyla eklendi!" };
                            }
                        }
                    }

                    else
                    {
                        return Json("Bir sorun oluştu! Lütfen bilgileri kontrol ediniz.");
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
        public async Task<IActionResult> ApproveEgitmen(EgitmenDto egitmen)
        {
            var response = new { success = false, message = "" };

            if (egitmen != null)
            {
                try
                {
                    var egitmenToApprove = await egitmenManager.GetByID(egitmen.ID);

                    if (egitmenToApprove != null)
                    {
                        egitmenToApprove.Durum = (EgitmenDurum)1;
                        egitmenManager.Update(egitmenToApprove);

                        response = new { success = true, message = "Eğitmen Onaylandı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Onaylanacak Eğitmen Bulunamadı" };
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
        public async Task<IActionResult> RemoveEgitmen(EgitmenDto egitmen)
        {
            var response = new { success = false, message = "" };

            if (egitmen != null)
            {
                try
                {
                    var egitmenToDelete = await egitmenManager.GetByID(egitmen.ID);

                    if (egitmenToDelete != null)
                    {
                        var allkurs = await kursManager.GetList();
                        var allkursdetails = await kursDetailManager.GetListWithIncludesAsync();
                        var kursforegitmen = allkurs.Where(p => p.EgitmenID == egitmen.ID).ToList();
                        var kursdetailsforegitmen = allkursdetails.Where(p => p.Kurs.EgitmenID == egitmen.ID).ToList();

                        foreach (var kurs in kursforegitmen)
                        {
                            kurs.Durum = (KursDurum)2;
                            await kursManager.Update(kurs);
                        }

                        foreach (var kursdetail in kursdetailsforegitmen)
                        {
                            kursdetail.Durum = (KursDetailDurum)2;
                            await kursDetailManager.Update(kursdetail);
                        }

                        egitmenToDelete.Durum = (EgitmenDurum)2;
                        egitmenManager.Update(egitmenToDelete);

                        response = new { success = true, message = "Eğitmen Kaldırıldı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Kaldırılacak Eğitmen Bulunamadı" };
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
        public async Task<IActionResult> DeleteEgitmen(EgitmenDto egitmen)
        {
            var response = new { success = false, message = "" };

            if (egitmen == null)
            {
                response = new { success = false, message = "Hata Oluştu" };
                return Json(response);
            }

            try
            {
                var egitmenToDelete = await egitmenManager.GetByID(egitmen.ID);
                if (egitmenToDelete == null)
                {
                    response = new { success = false, message = "Silinecek Eğitmen Bulunamadı" };
                    return Json(response);
                }

                var kurslar = (await kursManager.GetList()).Where(k => k.EgitmenID == egitmen.ID).ToList();
                var kursIDs = kurslar.Select(k => k.ID).ToList();

                var kursdetails = (await kursDetailManager.GetList()).Where(kd => kursIDs.Contains(kd.KursID)).ToList();
                var kursiyerkursdetails = (await kursiyerKursDetailManager.GetList()).Where(kkd => kursIDs.Contains(kkd.KursID)).ToList();

                foreach (var kursiyerkursdetail in kursiyerkursdetails)
                {
                    await kursiyerKursDetailManager.Delete(kursiyerkursdetail);
                }

                foreach (var kursdetail in kursdetails)
                {
                    await kursDetailManager.Delete(kursdetail);
                }

                foreach (var kurs in kurslar)
                {
                    await kursManager.Delete(kurs);
                }

                await egitmenManager.Delete(egitmenToDelete);

                response = new { success = true, message = "Eğitmen Silindi" };
            }
            catch (Exception ex)
            {
                response = new { success = false, message = $"Bilgileri Kontrol Ediniz: {ex.Message}" };
            }

            return Json(response);
        }




    }
}
