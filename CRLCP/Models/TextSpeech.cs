using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class TextSpeech
    {
        public long AutoId { get; set; }
        public int UserId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DataId { get; set; }
        public int LangId { get; set; }
        public int DomainId { get; set; }
        public byte[] OutputData { get; set; }
        public int DatasetId { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? TotalValidationUsersCount { get; set; }
        public int? VoteCount { get; set; }
        public int? IsValid { get; set; }
        public int? IsAddedInDataset { get; set; }
    }
}
