using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Models.Dashboard
{
    public class UserWiseDataCountModel
    {
        public int UserId { get; set; }
        public int DatasetId { get; set; }
        public int Datasetcount { get; set; }
        public string UserName { get; set; }
        public string DataSetName { get; set; }
    }
}
