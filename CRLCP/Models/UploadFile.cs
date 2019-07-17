using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Models
{
    public class UploadSpeechFile
    {
       
        public int Dataid { get; set; }

        public int UserId { get; set; }

        public int DatasetId { get; set; }
     
    }
}
