//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NancyAspNetHost
{
    using System;
    using System.Collections.Generic;
    
    public partial class Blok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Blok()
        {
            this.Tijd = new HashSet<Tijd>();
            this.Veld = new HashSet<Veld>();
        }
    
        public int Id { get; set; }
        public Nullable<int> EvenementID { get; set; }
        public int Nummer { get; set; }
        public System.DateTime StartTijd { get; set; }
        public System.DateTime EindTijd { get; set; }
        public bool Complete { get; set; }

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual Event Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<Tijd> Tijd { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<Veld> Veld { get; set; }
    }
}
