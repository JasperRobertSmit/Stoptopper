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
    
    public partial class Vereniging
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vereniging()
        {
            this.Event = new HashSet<Event>();
            this.Persoon = new HashSet<Persoon>();
            this.Ploeg = new HashSet<Ploeg>();
        }
    
        public int Id { get; set; }
        public string Afkorting { get; set; }
        public string Naam { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<Event> Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<Persoon> Persoon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnoreAttribute]
        public virtual ICollection<Ploeg> Ploeg { get; set; }
    }
}
