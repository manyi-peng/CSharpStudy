using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SpiderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BaiduImagTool.GetImage("二哈");
            Console.Read();
        }
    }

    public class BaiduImagTool
    {
        public static void GetImage(string word)
        {
            var url = "http://image.baidu.com/search/index?tn=baiduimage&ps=1&ct=201326592&lm=-1&cl=2&nc=1&ie=utf-8&word=" + Uri.EscapeDataString(word);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //解析html
            //doc.DocumentNode.SelectNodes("img");
            //Console.WriteLine(doc.ParsedText);
            File.Delete("a.html");
            if (!File.Exists("a.html"))
            {
                FileStream write = File.OpenWrite("a.html");
                var buffer = Encoding.UTF8.GetBytes(HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(url)));
                write.Write(buffer, 0, buffer.Length);
                write.Flush();
                write.Close();
            }
        }
    }

    public class HttpHelper
    {
        public static HttpWebResponse CreateGetHttpResponse(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.Referer = "";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36";
            return request.GetResponse() as HttpWebResponse;
        }
        /// 创建POST方式的HTTP请求  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36";

            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                request.ContentLength = data.Length;//这一步是设置body参数的长度，很重要
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);//这里是给body加参数
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                //Encoding.RegisterProvider(CodePagesEncodingProvider)
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("GB2312"));
                return reader.ReadToEnd();

            }
        }
    }

}
