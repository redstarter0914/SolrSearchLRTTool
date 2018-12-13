﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
   public class QueryModel
    {
        /// <summary>
        /// 打分方式  
        /// </summary>
        public string ScoreType { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 查询model
        /// </summary>
        public SearchModel SearchModel { get; set; }
    }
}