//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesHomes.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class HousingType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HousingType()
        {
            this.Housing = new HashSet<Housing>();
        }
    
        public int HousingTypeId { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]

        public virtual ICollection<Housing> Housing { get; set; }
    }
}
