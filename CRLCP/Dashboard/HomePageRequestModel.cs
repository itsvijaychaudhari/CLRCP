using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Models.Dashboard
{
    public class HomePageRequestModel
    {
        public bool IsAuthenticate { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public int DataSetId { get; set; }
        public int userType { get; set; }
    }
}
