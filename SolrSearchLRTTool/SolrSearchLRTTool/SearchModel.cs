using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
    public class SearchModel
    {
        /// <summary>
        /// 查询URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 查询类型
        /// </summary>
        public string SearchType { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string SearchKey { get; set; }
        /// <summary>
        /// 开始页数
        /// </summary>
        public int SearchStart { get; set; }
        /// <summary>
        /// 返回数量
        /// </summary>
        public int SearchNum { get; set; }
    }

    public enum EnumSearchType
    {
        lnsubstringbody2g,
        lnsubstringtitle2g,
        lnjudgeitemabstract
    }


    public class ExportDataModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string SearchKey { get; set; }
        /// <summary>
        /// 文档ID
        /// </summary>
        public string DocID { get; set; }
        /// <summary>
        /// 打分
        /// </summary>
        public decimal? Score { get; set; }      
        /// <summary>
        /// 打分类型
        /// </summary>
        public string ScoreType { get; set; }
    }
}
