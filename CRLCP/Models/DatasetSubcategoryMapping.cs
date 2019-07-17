using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class DatasetSubcategoryMapping
    {
        public int AutoId { get; set; }
        public int DatasetId { get; set; }
        public int SourceSubcategoryId { get; set; }
        public int DestinationSubcategoryId { get; set; }
    }
}
