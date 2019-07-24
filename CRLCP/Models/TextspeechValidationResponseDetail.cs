using System;
using System.Collections.Generic;

namespace CRLCP.Models
{
    public partial class TextspeechValidationResponseDetail
    {
        public long AutoId { get; set; }
        public int UserId { get; set; }
        public long RefAutoid { get; set; }
        public int IsMatch { get; set; }
        public int NoCrossTalk { get; set; }
        public int IsClear { get; set; }
        public int? ValidationFlag { get; set; }
    }
}
