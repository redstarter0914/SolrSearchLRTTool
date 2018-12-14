using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
    public class SearchData
    {

        /// <summary>
        /// 查询solr
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public static SolrResponseResult SearchResult(SearchModel queryInfo)
        {
            try
            {
                string reqBody = buildRequestURI(queryInfo);
                var stream = RestClientUtil.SendRequestJson(queryInfo.Url, reqBody, "application/x-www-form-urlencoded", "POST");
                return JsonExchange<SolrResponseResult>.ParseFormByJsonS(stream);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 生成 URL
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        private static string buildRequestURI(SearchModel queryInfo)
        {
            string qstr = "";
            //Body             Title            Abstract
            switch (queryInfo.SearchType)
            {
                case "Body":
                    qstr = EnumSearchType.lnsubstringtitle2g.ToString();
                    break;
                case "Title":
                    qstr = EnumSearchType.lnsubstringtitle2g.ToString();
                    break;
                case "Abstract":
                    qstr = EnumSearchType.lnjudgeitemabstract.ToString();
                    break;
            }
            string q = string.Format("{0}:({1})", qstr, queryInfo.SearchKey);
            string fl = "&fl=id,score";

            string rqstr = string.Format(@"q={0}&start={1}&rows={2}{3}", q, queryInfo.SearchStart, queryInfo.SearchNum, fl);

            return rqstr;

        }
    }
}
