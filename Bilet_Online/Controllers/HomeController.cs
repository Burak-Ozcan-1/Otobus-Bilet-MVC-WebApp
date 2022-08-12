using Bilet_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilet_Online.Controllers
{

    public class HomeController : Controller
    {
        Otobus_BiletEntities db = new Otobus_BiletEntities();
        EKLE ek = new EKLE();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }

        public ActionResult OnlineBilet()
        {
            Tuple<List<Guzergah>, List<Sefer>> degerler = new Tuple<List<Guzergah>, List<Sefer>>(db.Guzergah.ToList(), db.Sefer.ToList());
            
            ViewData["KALKIS_YERI"] = new SelectList(db.Guzergah.Select(c=> new { c.kalkis_yeri }).Distinct(), "KALKIS_YERI", "KALKIS_YERI");

            ViewData["VARIS_YERI"] = new SelectList(db.Guzergah, "VARIS_YERI", "VARIS_YERI");
            
            ViewBag.gosterilsinmi = 0;
            
            return View(degerler);
        }
        [HttpPost]
        public ActionResult OnlineBilet(List<string> yolcuadi, List<string> tc, FormCollection frm, string button, DateTime tarih, int sefer_id = 0, string kartno = " ", int sonkullanma_ay = 0, int sonkullanma_yil = 0, string kartsahibi = "", string guvenlik_kod = " ", string e_mail = " ", int cinsiyet = 0, string cep_tel = " ", decimal bilet_ucret = 0, string KALKIS_YERI = "", string VARIS_YERI = "")
        {
            List<int> koltuklar = new List<int>();
            List<string> yolcu_adi = new List<string>();
            List<string> ara_tc = new List<string>();
            string mesaj = "";
            decimal ucret = 0;
            var bul = db.Sefer.Where(p => p.sefer_id == sefer_id).ToList();
            foreach (var item in bul)
            {
                ViewBag.koltuk_tip = item.Otobus.koltuk_tipi;
                ViewBag.koltuk_sayisi = item.Otobus.koltuk_sayisi;
                ViewBag.ucret = Convert.ToDecimal(item.ucret - 5);
            }
            if (button == "satis")
            {
                ViewBag.kredikartigoster = 1;
                for (int i = 1; i <= ViewBag.koltuk_sayisi; i++)
                {
                    string id = "koltuklar_" + i;
                    string asss = frm[id];
                    if (asss != null)
                    {
                        koltuklar.Add(i);
                    }
                }
                foreach (var item in yolcuadi)
                {
                    if (item != "")
                        yolcu_adi.Add(item);
                }
                foreach (var item in tc)
                {
                    if (item != "")
                        ara_tc.Add(item);
                }
                if (koltuklar.Count() != yolcu_adi.Count() || koltuklar.Count() != ara_tc.Count())
                    ViewBag.tc_yolcuadi = 1;

                for (int i = 0; i < koltuklar.Count(); i++)
                {
                    mesaj = ek.BiletEkle(koltuklar, yolcu_adi[i], cep_tel, e_mail, cinsiyet, Convert.ToInt64(ara_tc[i]), sefer_id, bilet_ucret, KALKIS_YERI, VARIS_YERI, kartno, kartsahibi, sonkullanma_ay + " " + sonkullanma_yil, guvenlik_kod);
                }
                if (mesaj != "HATA") 
                {
                    return RedirectToAction("OdemeAlindi", "Home");
                }

            }

            List<int> asd = new List<int>();
            int j = 0;
            for (int i = 1; i <= ViewBag.koltuk_sayisi; i++)
            {
                string id = "koltuk_" + i;
                string asss = frm[id];
                if (asss != null)
                {
                    if (asd.Count() > 4)
                    {
                        ViewBag.HataIki = 1;
                    }
                    else
                    {
                        asd.Add(i);
                        j++;
                        ViewBag.HataIki = 0;
                    }
                }
            }

            var toplam = asd.ToList();
            ViewBag.secilenler = toplam;
            int sayi = 0;
            foreach (var item in toplam)
            {
                if (item != 0)
                {
                    sayi++;
                }
            }
            ViewBag.toplam = sayi;

            if (button == "ara")
            {
                if (KALKIS_YERI == "" || VARIS_YERI == "")
                {
                    ViewBag.Hatadort = 1;
                }
                ViewBag.sefer_goster = 1;
            }
            if (button == "DEVAM ET")
            {
                if (cinsiyet == 0)
                {
                    ViewBag.HataUc = 1;
                }
                else
                {
                    if (sayi == 0)
                    {
                        ViewBag.Hata = 1;
                        ViewBag.HataUc = 0;
                    }
                    else
                    {
                        ViewBag.kredikartigoster = 1;
                        ViewBag.Hata = 0;
                        sefer_id = Convert.ToInt32(frm["sefer_id"]);
                        var bul_iki = db.Sefer.Where(p => p.sefer_id == sefer_id).ToList();
                        foreach (var item in bul_iki)
                        {
                            ucret = Convert.ToDecimal(item.ucret - 5) * sayi;
                            ViewBag.koltuk_tip = item.Otobus.koltuk_tipi;
                        }
                        ViewBag.yeni_ucret = ucret;
                        ViewBag.gosterilsinmi = 1;
                        ViewBag.sefer_goster = 1;
                        ViewBag.HataUc = 0;
                    }
                }
            }


            Tuple<List<Guzergah>, List<Sefer>> degerler = new Tuple<List<Guzergah>, List<Sefer>>(db.Guzergah.Where(p => p.kalkis_yeri == KALKIS_YERI && p.varis_yeri == VARIS_YERI).ToList(), db.Sefer.Where(p => p.kalkis_tarihi == tarih).ToList());

            ViewData["KALKIS_YERI"] = new SelectList(db.Guzergah.Select(c => new { c.kalkis_yeri }).Distinct(), "KALKIS_YERI", "KALKIS_YERI");

            ViewData["VARIS_YERI"] = new SelectList(db.Guzergah, "VARIS_YERI", "VARIS_YERI");
            
            if (sefer_id != 0)
            {
                ViewBag.gosterilsinmi = 1;
                ViewBag.sefer_goster = 1;
                ViewBag.seferid = sefer_id;
                ViewBag.cinsiyet = cinsiyet;
            }

            return View(degerler);
            
        }

        public ActionResult Odeme()
        {
            Tuple<List<Guzergah>, List<Sefer>> degerler = new Tuple<List<Guzergah>, List<Sefer>>(db.Guzergah.ToList(), db.Sefer.ToList());
            return View(degerler);
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult OdemeAlindi()
        {
            return View();
        }

    }
}