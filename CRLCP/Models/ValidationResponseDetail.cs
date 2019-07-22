using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class ValidationResponseDetail
    {
        public long AutoId { get; set; }
        public long RefAutoid { get; set; }
        public int SubcategoryId { get; set; }
        public int ValidationFlag { get; set; }
        public string ValidationDetail { get; set; }
    }
}
