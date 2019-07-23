using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Models
{
    public class ValidationTextSpeechModel
    {
        public long DestAutoId { get; set; }
        
        public byte[] DestinationData{ get; set; }

        public int SourceDataId { get; set; }
        public string SourceData { get; set; }

        public int DatasetID { get; set; }
    }
}
