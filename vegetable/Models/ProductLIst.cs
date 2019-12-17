using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace vegetable.Models
{
    public class SearchCondition
    {
        public string Condition { get; set; }
        public int Page { get; set; }
        public int? OrderBy { get; set; }
    }

    
}