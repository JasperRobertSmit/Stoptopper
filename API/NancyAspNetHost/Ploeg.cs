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
    
    public partial class Ploeg
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ploeg()
        {
            this.PloegDeelnemer = new HashSet<PloegDeelnemer>();
            this.Tijd = new HashSet<Tijd>();
        }
    
        public int Id { get; set; }
        public string Rugnummer { get; set; }
        public string Naam { get; set; }
        public Nullable<int> VerenigingID { get; set; }
        public Nullable<int> VeldID { get; set; }
        public string Bootnaam { get; set; }

        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual Veld Veld { get; set; }
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual Vereniging Vereniging { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<PloegDeelnemer> PloegDeelnemer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<Tijd> Tijd { get; set; }
    }
}