using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int QualificationId { get; set; }
        public int LangId1 { get; set; }
        public int LangId2 { get; set; }
        public int LangId3 { get; set; }
    }
}
