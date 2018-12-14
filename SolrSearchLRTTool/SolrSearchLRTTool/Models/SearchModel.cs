using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
    /// <summary>
    /// 查询solr模型
    /// </summary>
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
    /// <summary>
    /// solr字段查询枚举
    /// </summary>
    public enum EnumSearchType
    {
        /// <summary>
        /// Body
        /// </summary>
        lnsubstringbody2g,
        /// <summary>
        /// Title
        /// </summary>
        lnsubstringtitle2g,
        /// <summary>
        /// Abstract
        /// </summary>
        lnjudgeitemabstract
    }

    /// <summary>
    /// 导入打分数据模型
    /// </summary>
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
