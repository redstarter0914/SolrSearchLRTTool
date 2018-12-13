using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearchLRTTool
{
    public class SolrResponseResult
    {
        /// <summary>
        /// 返回 Header
        /// </summary>
        public ResponseHeader ResponseHeader { get; set; }
        /// <summary>
        /// 返回response
        /// </summary>
        public Response<SolrDocModel> Response { get; set; } 
        //  public object Highlighting { get; set; }
        private List<string> hls = new List<string>(); 
        /// <summary>
        /// 错误提示
        /// </summary>
        public Error Error { get; set; }
         
    }

    /// <summary>
    /// 返回头
    /// </summary>
    public class ResponseHeader
    {
        [JsonProperty(PropertyName = "zkConnected", DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ZKConnected { get; set; }
        [JsonProperty(PropertyName = "status", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Status { get; set; }
        public int QTime { get; set; }
        public ParamsC Paramsc { get; set; }

    }

    public class ParamsC
    {
        [JsonProperty(PropertyName = "q", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Q { get; set; }
        [JsonProperty(PropertyName = "hla", DefaultValueHandling = DefaultValueHandling.Include)]
        public string HLA { get; set; }
        [JsonProperty(PropertyName = "hl", DefaultValueHandling = DefaultValueHandling.Include)]
        public HL HL { get; set; }
        [JsonProperty(PropertyName = "indent", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Indent { get; set; }
        [JsonProperty(PropertyName = "row", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Row { get; set; }
        [JsonProperty(PropertyName = "wt", DefaultValueHandling = DefaultValueHandling.Include)]
        public string WT { get; set; }

    }

    public class HL
    {
        [JsonProperty(PropertyName = "hls", DefaultValueHandling = DefaultValueHandling.Include)]
        public string HLs { get; set; }
        [JsonProperty(PropertyName = "simple", DefaultValueHandling = DefaultValueHandling.Include)]
        public Simple Simple { get; set; }

    }

    public class Simple
    {
        [JsonProperty(PropertyName = "post {", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Post { get; set; }
        [JsonProperty(PropertyName = "pre", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Pre { get; set; }

    }
    /// <summary>
    /// 返回response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<SolrDocModel>
    {
        [JsonProperty(PropertyName = "numFound", DefaultValueHandling = DefaultValueHandling.Include)]
        public int NumFound { get; set; }
        [JsonProperty(PropertyName = "start", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Start { get; set; }
        [JsonProperty(PropertyName = "maxScore", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal MaxScore { get; set; }
        [JsonProperty(PropertyName = "docs", DefaultValueHandling = DefaultValueHandling.Include)]
        public List<SolrDocModel> Docs { get; set; }
    }
    /// <summary>
    /// 高亮显示
    /// </summary> 
    public class Highlighting
    {
        // public List<string> initial_release_date { get; set; } 

    }
    /// <summary>
    /// 错误结果
    /// </summary>
    public class Error
    {
        [JsonProperty(PropertyName = "metadata", DefaultValueHandling = DefaultValueHandling.Include)]
        public List<string> MetaData { get; set; }
        [JsonProperty(PropertyName = "msg", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Msg { get; set; }
        [JsonProperty(PropertyName = "code", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Code { get; set; }
    }

    public class SolrDocModel
    {
        [JsonProperty(PropertyName = "id", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "score", DefaultValueHandling = DefaultValueHandling.Include)]
        public decimal Score { get; set; }
    }
}
