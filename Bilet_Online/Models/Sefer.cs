//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bilet_Online.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sefer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sefer()
        {
            this.Bilet = new HashSet<Bilet>();
            this.Rezerve = new HashSet<Rezerve>();
            this.Sefer_Arayol = new HashSet<Sefer_Arayol>();
        }
    
        public int sefer_id { get; set; }
        public string kalkis_saat { get; set; }
        public Nullable<int> guzergah_id { get; set; }
        public Nullable<System.DateTime> kalkis_tarihi { get; set; }
        public Nullable<decimal> ucret { get; set; }
        public Nullable<int> otobus_id { get; set; }
        public string peron_no { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bilet> Bilet { get; set; }
        public virtual Guzergah Guzergah { get; set; }
        public virtual Otobus Otobus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezerve> Rezerve { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sefer_Arayol> Sefer_Arayol { get; set; }
    }
}
