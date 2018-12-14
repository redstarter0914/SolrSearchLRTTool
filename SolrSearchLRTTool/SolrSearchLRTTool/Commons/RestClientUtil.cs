using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions; 
using System.Diagnostics;
using System.IO;

namespace SolrSearchLRTTool
{
    public class RestClientUtil
    {
        #region  json
        /// <summary>
        /// 根据解析后的字符串，解析是否正确
        /// </summary>
        /// <param name="newstr"></param>
        /// <param name="msg"></param>
        /// <returns></returns> 
        public static Stream SendRequestJson(string url, string bodystr, string contentType, string method)
        {
            try
            {
                var request = WebRequest.Create(url);

                request.Method = method;// "POST";
                request.ContentType = contentType;// "application/x-www-form-urlencoded";
                request.Proxy = null;
                request.Timeout = 500000;

                string param = bodystr + "&wt=json";

                byte[] bs = Encoding.UTF8.GetBytes(param);
                request.ContentLength = bs.Length;

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                Stream stream = new MemoryStream();
                {
                    using (WebResponse wr = request.GetResponse())
                    {
                        wr.GetResponseStream().CopyTo(stream);
                        wr.Close();
                    }

                    request.Abort();
                    request = null;
                    return stream;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetStreamToStr(Stream stream)
        {
            string result = "";

            if (stream.CanRead)
            {
                using (StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                {
                    stream.Position = 0;
                    result = sr.ReadToEnd();
                    sr.Close();
                }
            }
            return result;
        }

        #endregion
    }
}
