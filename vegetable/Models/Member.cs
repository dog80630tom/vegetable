using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberGender { get; set; }
        public string MemberPhone { get; set; }
        public string MemberPassword { get; set; }
    }
}