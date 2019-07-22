using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class TextText
    {
        public int AutoId { get; set; }
        public int UserId { get; set; }
        public int DataId { get; set; }
        public int LangId { get; set; }
        public int DomainId { get; set; }
        public string OutputData { get; set; }
        public int OutputLangId { get; set; }
        public int DatasetId { get; set; }
        public DateTime AddedOn { get; set; }
        public int? IsValid { get; set; }
    }
}
