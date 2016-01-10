using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Aostar.MVP.WebClient
{
    public class HttpClient : System.Net.WebClient
    {
        // Cookie 容器
        private int _defaultConnectionLimit = 100;
        private int _timeout = 30;
        private bool allowAutoRedirect = true;
        private CookieContainer cookieContainer;


        /// <summary>
        ///     创建一个新的 WebClient 实例。
        /// </summary>
        public HttpClient()
        {
            cookieContainer = new CookieContainer();
            ServicePointManager.Expect100Continue = false;

            if (DefaultConnectionLimit > 0)
            {
                ServicePointManager.DefaultConnectionLimit = DefaultConnectionLimit; //设置并发连接数限制上额
            }
        }



        // <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        /// <param name="cookie">Cookie 容器</param>
        public HttpClient(CookieContainer cookies)
            : this()
        {
            if (cookies != null)
            {
                cookieContainer = cookies;
            }
        }

        public int DefaultConnectionLimit
        {
            get { return _defaultConnectionLimit; }
            set { _defaultConnectionLimit = value; }
        }

        /// <summary>
        ///     页面是否允许自动跳转，302代码
        /// </summary>
        public bool AllowAutoRedirect
        {
            get { return allowAutoRedirect; }
            set { allowAutoRedirect = value; }
        }

        /// <summary>
        ///     Cookie 容器
        /// </summary>
        public CookieContainer Cookies
        {
            get { return cookieContainer; }
            set { cookieContainer = value; }
        }

        /// <summary>
        ///     超时间间(秒)
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }


        /// <summary>
        ///     返回带有 Cookie 的 HttpWebRequest。
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.Timeout = Timeout * 1000;
                httpRequest.AllowAutoRedirect = allowAutoRedirect;
                httpRequest.CookieContainer = cookieContainer;
            }
            return request;
        }


        public List<Cookie> GetCookies()
        {
            List<Cookie> cookieCollection = new List<Cookie>();
            try
            {
                Hashtable m_domainTable = (Hashtable)cookieContainer.GetType().InvokeMember("m_domainTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, cookieContainer, new object[] { });

                //通过反射CookieContainer类进入其内部对私有变量获取其值。m_domainTable为CookieContainer类中的私有字段，类型为Hashtable
                foreach (object pathList in m_domainTable.Values)
                {
                    //pathList为一个SortList类型的派生类
                    SortedList m_list = (SortedList)pathList.GetType().InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                    foreach (CookieCollection cookies in m_list.Values)
                        foreach (Cookie cookie in cookies)
                        {
                            if (!cookieCollection.Contains(cookie)) cookieCollection.Add(cookie);
                        }
                }
            }
            catch
            {
            }
            return cookieCollection;
        }
    }
}