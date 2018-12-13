using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
    public class QueryExcelModel
    {
        public int QueryID { get; set; }
        public string QueryString { get; set; }

    }


    public class QueryExcelModelNoData
    {
        public int QueryID { get; set; }
        public string QueryString { get; set; }
        public string ErrorType { get; set; } 
    }
}
