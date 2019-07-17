using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class LoginDetails
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
}
