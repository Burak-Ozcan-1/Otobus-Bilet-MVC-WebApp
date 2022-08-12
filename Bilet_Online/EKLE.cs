using Bilet_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bilet_Online
{
    public class EKLE
    {
        Otobus_BiletEntities db = new Otobus_BiletEntities();
        public string BiletEkle(List<int> koltuk, string adi_soyadi,string tel, string e_posta, int cinsiyet_kod,long tc,int sefer_id, decimal ucret, string kalkis,string varis, string kartno,string kartsahibi,string son_kullanma, string guvenlik_kod)
        {
            try
            {
                for (int i = 0; i < koltuk.Count(); i++)
                {

                    var yolcu_ekle = new Yolcu
                    {
                        adi_soyadi = adi_soyadi,
                        Tel = tel,
                        e_posta = e_posta,
                        cinsiyet_kod = cinsiyet_kod,
                        TC = tc
                    };
                    db.Yolcu.Add(yolcu_ekle);
                    db.SaveChanges();
                    int yolcu_id = db.Yolcu.Where(p => p.adi_soyadi == adi_soyadi).Select(p=>p.yolcu_id).SingleOrDefault();
                    var bilet_ekle = new Bilet
                    {
                        koltuk_no = koltuk[i],
                        yolcu_id = yolcu_id,
                        sefer_id = sefer_id,
                        indirim_id = 6,
                        bilet_kesen_personel_id = 4,
                        islem_kod = 33, // kredi kartı 
                        satis_tarihi = System.DateTime.Now.ToShortDateString(),
                        tutar = ucret,
                        kalkis = kalkis,
                        varis = varis,
                        online = 1,
                        rezerve = 0,
                        iptal = 0
                    };
                    db.Bilet.Add(bilet_ekle);
                    var kredi_karti_ekle = new Kredi_Karti
                    {
                        kart_no = kartno,
                        kart_sahibi_adi_soyadi = kartsahibi,
                        islem_tarih = System.DateTime.Now.ToShortDateString(),
                        son_kullanma_tarihi = son_kullanma,
                        guvenlik_kodu = guvenlik_kod
                    };
                    db.Kredi_Karti.Add(kredi_karti_ekle);
                    db.SaveChanges();
                    return "Kayıt İşlemi Tamam";
                }
                return "HATA!";
            }
            catch (Exception)
            {
                return "HATA!";
            }
        }
    }
}