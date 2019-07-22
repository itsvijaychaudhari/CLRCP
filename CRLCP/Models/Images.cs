using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class Images
    {
        public long DataId { get; set; }
        public byte[] Image { get; set; }
        public int? SourceId { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? LangId { get; set; }
        public int DomainId { get; set; }
        public int? DatasetId { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
