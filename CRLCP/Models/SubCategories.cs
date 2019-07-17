using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class SubCategories
    {
        public int SubcategoryId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
