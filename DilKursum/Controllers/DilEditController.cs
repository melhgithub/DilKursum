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
    public class DilEditController : Controller
    {
        DilManager dilManager = new DilManager(new EFDilRepository());
        EgitmenManager egitmenManager = new EgitmenManager(new EFEgitmenRepository());
        KursManager kursManager = new KursManager(new EFKursRepository());
        KursDetailManager kursDetailManager = new KursDetailManager(new EFKursDetailRepository());
        KursiyerKursDetailManager kursiyerKursDetailManager = new KursiyerKursDetailManager(new EFKursiyerKursDetailRepository());
        public async Task<IActionResult> Index()
        {
            var diller = await dilManager.GetList();
            var dto = new DilDto();
            var model = new DilModel
            {
                Diller = diller,
                DTO = dto
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterDil(DilDto dil)
        {
            var diller = await dilManager.GetList();

            if (!string.IsNullOrEmpty(dil.DilAdi))
            {
                var dilAdiLower = dil.DilAdi.ToLower();
                diller = diller.Where(k => k.DilAdi.ToLower().Contains(dilAdiLower)).ToList();
            }

            if (dil.Durum != 0)
            {
                diller = diller.Where(d => d.Durum == dil.Durum).ToList();
            }

            var dilData = diller.Select(p => new
            {
                ID = p.ID,
                Diladi = p.DilAdi,
                Durum = p.Durum
            });

            return Json(dilData);
        }


        [HttpPost]
        public async Task<IActionResult> Filter(DilDto dil)
        {
            var diller = await dilManager.GetList();

            if (!string.IsNullOrEmpty(dil.DilAdi))
            {
                diller = diller.Where(d => d.DilAdi.Contains(dil.DilAdi)).ToList();
            }

            if (dil.Durum>0)
            {
                diller = diller.Where(d => d.Durum == dil.Durum).ToList();
            }

            var dilData = diller.Select(p => new
            {
                ID = p.ID,
                Diladi = p.DilAdi,
                Durum = p.Durum,
            });

            return Json(dilData);
        }


        [HttpPost]
        public async Task<IActionResult> AddEditDil(DilDto dil)
        {
            var response = new { success = false, message = "" };

            if (dil != null)
            {
                try
                {
                    var entity = dil.ID > 0 ? await dilManager.GetByID(dil.ID) : new Dil();

                    entity.DilAdi = dil.DilAdi;
                    entity.Durum = dil.Durum;


                    if (entity.ID > 0)
                    {
                        await dilManager.Update(entity);
                        response = new { success = true, message = "Dil başarıyla güncellendi!" };
                    }
                    else
                    {
                        var dilAdiSorgu = await dilManager.GetDilIdByDilAdi(entity.DilAdi);
                        var dilAdiS = await dilManager.GetByID(dilAdiSorgu);
                        if (dilAdiSorgu>0)
                        {
                            response = new { success = true, message = "Dil Adını Kontrol Ediniz!" };
                        }
                        else
                        {
                            await dilManager.Add(entity);
                            response = new { success = true, message = "Dil başarıyla eklendi!" };
                        }
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
        public async Task<IActionResult> ApproveDil(DilDto dil)
        {
            var response = new { success = false, message = "" };

            if (dil != null)
            {
                try
                {
                    var dilToApprove = await dilManager.GetByID(dil.ID);

                    if (dilToApprove != null)
                    {
                        dilToApprove.Durum = (DilDurum)1;
                        dilManager.Update(dilToApprove);

                        response = new { success = true, message = "Dil Onaylandı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Onaylanacak Dil Bulunamadı" };
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
        public async Task<IActionResult> RemoveDil(DilDto dil)
        {
            var response = new { success = false, message = "" };

            if (dil != null)
            {
                try
                {
                    var dilToDelete = await dilManager.GetByID(dil.ID);

                    if (dilToDelete != null)
                    {
                        dilToDelete.Durum = (DilDurum)2;
                        dilManager.Update(dilToDelete);

                        response = new { success = true, message = "Dil Kaldırıldı" };
                    }
                    else
                    {
                        response = new { success = false, message = "Kaldırılacak Dil Bulunamadı" };
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
        public async Task<IActionResult> DeleteDil(DilDto dil)
        {
            var response = new { success = false, message = "" };

            if (dil == null)
            {
                response = new { success = false, message = "Hata Oluştu" };
                return Json(response);
            }

            try
            {
                var dilToDelete = await dilManager.GetByID(dil.ID);
                if (dilToDelete == null)
                {
                    response = new { success = false, message = "Silinecek Dil Bulunamadı" };
                    return Json(response);
                }

                var kurslar = (await kursManager.GetList()).Where(k => k.DilID == dil.ID).ToList();
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

                await dilManager.Delete(dilToDelete);

                response = new { success = true, message = "Dil Silindi" };
            }
            catch (Exception ex)
            {
                response = new { success = false, message = $"Bilgileri Kontrol Ediniz: {ex.Message}" };
            }

            return Json(response);
        }



    }
}
