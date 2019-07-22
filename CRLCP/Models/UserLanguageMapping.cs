using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class UserLanguageMapping
    {
        public int AutoId { get; set; }
        public int LanguageId { get; set; }
        public int UserId { get; set; }
    }
}
