using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SolrSearchLRTTool
{
    /// <summary>
    /// Json 与 T 之间的转化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonExchange<T>
    {
        /// <summary>
        /// 将类型实例转换为Json字符串
        /// </summary>
        /// <param name="t">类型实例</param>
        /// <returns></returns>
        public static string ToJsonString(T t)
        {
            StringBuilder sb = new StringBuilder();
            using (TextWriter tw = new StringWriter(sb))
            {
                using (JsonWriter writer = new JsonTextWriter(tw))
                {
                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, t);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将List转换为字符串
        /// </summary>
        /// <param name="list">数据集</param>
        /// <returns>字符串</returns>
        public static string ToJsonString(List<T> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 将Json字符串转换为类型实例
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T ParseFormByJson(Stream jsonString)
        {
            try
            {
                jsonString.Position = 0;
                using (StreamReader tr = new StreamReader(jsonString))
                using (JsonReader reader = new JsonTextReader(tr))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<T>(reader);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
            finally
            {
                jsonString.Close();
                jsonString.Dispose();
            }
        }

        public static T ParseFormByJsonS(Stream jsonString)
        {
            try
            {
                string result = "";
                if (jsonString.CanRead)
                {
                    using (StreamReader sr = new StreamReader(jsonString, Encoding.GetEncoding("UTF-8")))
                    {
                        jsonString.Position = 0;
                        result = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                result = result.Replace("<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">", "").Replace("</string>", "");
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                return default(T);
            }
            finally
            {
                jsonString.Close();
                jsonString.Dispose();
            }
        }

        /// <summary>
        /// 将Json字符串转换为类型实例
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static List<T> ParseFormByJsonList(Stream jsonString)
        {
            try
            {
                jsonString.Position = 0;
                using (StreamReader tr = new StreamReader(jsonString))
                using (JsonReader reader = new JsonTextReader(tr))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<List<T>>(reader);
                }
            }
            catch (Exception ex)
            {
                return default(List<T>);
            }
            finally
            {
                jsonString.Close();
                jsonString.Dispose();
            }
        }

        /// <summary>
        /// 将Json字符串转换为类型实例
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T ParseFormByJson(string jsonString)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将Json字符串转换为类型实例
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static List<T> ParseFormByJsonList(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonString);

            }
            catch (Exception ex)
            {
                return default(List<T>);
            }
        }
    }
}
