//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TRPO_Cursach
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRPO_Provider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRPO_Provider()
        {
            this.TRPO_Product = new HashSet<TRPO_Product>();
        }
    
        public int id_provider { get; set; }
        public string name { get; set; }
        public string legal_address { get; set; }
        public string contact_number { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRPO_Product> TRPO_Product { get; set; }
    }
}
