using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolrSearchLRTTool
{
    public static class KeyWordReplaceHelper
    {
        /// <summary>
        /// andnot转换
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceAndnotS(this string faststr)
        {
            if (faststr.ToLower().Contains(ConstantHelper.AndNotStr))
            {
                return faststr.Replace("andnot", "￥").Replace("ANDNOT", "￥").Replace("ANDnot", "￥").Replace("andNOT", "￥");
            }

            return faststr;
        }


        /// <summary>
        /// and转换
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceAndS(this string faststr)
        {
            if (faststr.ToLower().Contains(ConstantHelper.AndStr))
            {
                return faststr.Replace("and", "￥").Replace("AND", "￥");
            }

            return faststr;
        }
        /// <summary>
        /// or转换
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceOrS(this string faststr)
        {
            if (faststr.ToLower().Contains(ConstantHelper.OrStr))
            {
                return faststr.Replace("or", "￥").Replace("OR", "￥");
            }

            return faststr;
        }

        /// <summary>
        /// not转换
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceNotS(this string faststr)
        {
            if (faststr.ToLower().Contains(ConstantHelper.NotStr))
            {
                return faststr.Replace("not", "￥").Replace("NOT", "￥");
            }

            return faststr;
        }

        /// <summary>
        /// Near转换
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceNearS(this string faststr)
        {
            if (faststr.ToLower().Contains(ConstantHelper.NearStr))
            {
                return faststr.Replace("near", "￥near").Replace("NEAR", "near￥");
            }

            return faststr;
        }

        /// <summary>
        /// Brackets字符串  (  
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceFBracketsS(this string faststr)
        {
            return faststr.Replace("(", "").Replace("（", "");
        }
        /// <summary>
        ///  Brackets字符串  )
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceEBracketsS(this string faststr)
        {
            return faststr.Replace(")", "").Replace("）", "");
        }
        /// <summary>
        /// Keyword 拼接字符串
        /// </summary>
        /// <param name="faststr"></param>
        /// <returns></returns>
        public static string ReplaceALLByKeyword(this string faststr)
        {
            string Result = "";
            var str = faststr.ReplaceAndS().ReplaceAndnotS().ReplaceEBracketsS().ReplaceFBracketsS().ReplaceNearS().ReplaceNotS().ReplaceOrS();
            var strs = str.Split('￥').Distinct();
            foreach (var d in strs)
            {
                if (!string.IsNullOrEmpty(d.Trim()) && !d.Contains(ConstantHelper.NearStr))
                {
                    Result = string.Format("{0} \"{1}\"", Result, d.Trim());
                }
            }

            return Result;
        }
    }
}
