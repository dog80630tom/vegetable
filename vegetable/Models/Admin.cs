using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class Admin
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Admin()
        //{
        //    this.AdminResponds = new HashSet<AdminRespond>();
        //}
    
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string AdminPhone { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AdminRespond> AdminResponds { get; set; }
    }
}
