using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class DatasetSubcategoryMappingValidation
    {
        public int AutoId { get; set; }
        public int? DatasetId { get; set; }
        public int? DestinationSubcategoryId { get; set; }
    }
}
