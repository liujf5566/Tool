using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Aostar.MVP.WebClient
{
    public class HttpClient : System.Net.WebClient
    {
        // Cookie ����
        private int _defaultConnectionLimit = 100;
        private int _timeout = 30;
        private bool allowAutoRedirect = true;
        private CookieContainer cookieContainer;


        /// <summary>
        ///     ����һ���µ� WebClient ʵ����
        /// </summary>
        public HttpClient()
        {
            cookieContainer = new CookieContainer();
            ServicePointManager.Expect100Continue = false;

            if (DefaultConnectionLimit > 0)
            {
                ServicePointManager.DefaultConnectionLimit = DefaultConnectionLimit; //���ò��������������϶�
            }
        }



        // <summary>
        /// ����һ���µ� WebClient ʵ����
        /// </summary>
        /// <param name="cookie">Cookie ����</param>
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
        ///     ҳ���Ƿ������Զ���ת��302����
        /// </summary>
        public bool AllowAutoRedirect
        {
            get { return allowAutoRedirect; }
            set { allowAutoRedirect = value; }
        }

        /// <summary>
        ///     Cookie ����
        /// </summary>
        public CookieContainer Cookies
        {
            get { return cookieContainer; }
            set { cookieContainer = value; }
        }

        /// <summary>
        ///     ��ʱ���(��)
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }


        /// <summary>
        ///     ���ش��� Cookie �� HttpWebRequest��
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

                //ͨ������CookieContainer��������ڲ���˽�б�����ȡ��ֵ��m_domainTableΪCookieContainer���е�˽���ֶΣ�����ΪHashtable
                foreach (object pathList in m_domainTable.Values)
                {
                    //pathListΪһ��SortList���͵�������
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