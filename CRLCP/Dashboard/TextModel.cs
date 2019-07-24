using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Dashboard
{
    public class TextModel
    {
        public int DataId { get; set; }
        public string Text1 { get; set; }
        public int SourceId { get; set; }
        public DateTime AddedOn { get; set; }
        public int LangId { get; set; }
        public int DomainId { get; set; }
        public int DatasetId { get; set; }
        public string NewDomainToAdd { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
