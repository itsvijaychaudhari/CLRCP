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
        public int? SourceSubcategoryId2 { get; set; }
        public int? DestinationSubcategoryId2 { get; set; }
        public int? SourceSubcategoryId3 { get; set; }
        public int? DestinationSubcategoryId3 { get; set; }
    }
}
