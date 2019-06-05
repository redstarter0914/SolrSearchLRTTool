using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool.Models
{
    public class SearchDataModel
    {
        public List<ExportDataModel> searchData { get; set; }

        public List<QueryExcelModelNoData> queryResultList { get; set; }
    }
}
