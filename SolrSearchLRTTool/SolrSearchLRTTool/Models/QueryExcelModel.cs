using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
    /// <summary>
    /// Excel 读取模板
    /// </summary>
    public class QueryExcelModel
    {
        /// <summary>
        /// 查询ID
        /// </summary>
        public int QueryID { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string QueryString { get; set; }

    }
    
    /// <summary>
    /// Excel 写入模板
    /// </summary>
    public class QueryExcelModelNoData
    {   /// <summary>
        /// 查询ID
        /// </summary>
        public int QueryID { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string QueryString { get; set; }
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; } 
    }
}
