using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRLCP.Models.Dashboard
{
    public class HomePageModel
    {
        public string DataSetName { get; set; }

        public int SourceDataCount { get; set; }

        public int CollectedDataCount { get; set; }
    }
}
