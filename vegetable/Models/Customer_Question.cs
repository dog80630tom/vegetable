using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class Customer_Question
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Customer_Question()
        //{
        //    this.AdminResponds = new HashSet<AdminRespond>();
        //}
        [Key]
        public int CQ_ID { get; set; }
        public int ProductID { get; set; }
        public string CQ_Question { get; set; }
        public System.TimeSpan CQ_Time { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AdminRespond> AdminResponds { get; set; }
        //public virtual Member Member { get; set; }
    }
}