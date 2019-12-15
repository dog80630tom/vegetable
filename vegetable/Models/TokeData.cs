using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vegetable.Models
{
    public class TokeData
    {
        public string access_token { get; set; }
        /// <summary>
        /// refresh_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// expires_in
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        /// token_type
        /// </summary>
        public string token_type { get; set; }
    }
}