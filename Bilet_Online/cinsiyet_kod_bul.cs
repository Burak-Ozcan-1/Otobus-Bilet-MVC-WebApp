using Bilet_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bilet_Online
{
    
    public class cinsiyet_kod_bul
    {
        Otobus_BiletEntities db = new Otobus_BiletEntities();

        public int cinsiyet(int yolcu_id)
        {
            var f = db.Yolcu.Where(p => p.yolcu_id == yolcu_id).ToList();
            foreach (var item in f)
            {
                return Convert.ToInt32(item.cinsiyet_kod);
            }
            return 0;
        }
    }
}