using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class Datasets
    {
        public int DatasetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MaxCollectionUsers { get; set; }
        public int? MaxValidationUsers { get; set; }
        public int? IsVisible { get; set; }
    }
}
