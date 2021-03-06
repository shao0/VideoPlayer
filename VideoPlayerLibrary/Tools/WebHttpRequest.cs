using System;
using System.Collections;
using System.Threading.Tasks;
using RestSharp;

namespace VideoPlayerLibrary.Tools
{
    public static class WebHttpRequest
    {

        /// <summary>
        /// HTTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求类型/Post/Get</param>
        /// <param name="header">头文件</param>
        /// <param name="parameter">参数</param>
        /// <returns>是否成功</returns>
        public static async Task<string> GetStringAsync(string url, Method method = Method.GET,
            Hashtable header = null, string parameter = null)
        {
            string result = string.Empty;
            try
            {
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(method);
                if (header != null)
                {
                    foreach (object headerKey in header.Keys)
                    {
                        request.AddHeader(headerKey.ToString(), header[headerKey].ToString());
                    }
                }
                if (method == Method.POST && parameter != null)
                {
                    request.AddParameter("application/json", parameter, ParameterType.RequestBody);
                }
                IRestResponse response = await client.ExecuteAsync(request);
                result = response.Content;
            }
            catch (Exception e)
            {

            }
            return result;
        }
        /// <summary>
        /// HTTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求类型/Post/Get</param>
        /// <param name="header">头文件</param>
        /// <param name="parameter">参数</param>
        /// <returns>是否成功</returns>

        public static async Task<byte[]> GetByteAsync(string url, Method method = Method.GET,
            Hashtable header = null, string parameter = null)
        {
            byte[] result = null;
            try
            {
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(method);
                if (header != null)
                {
                    foreach (object headerKey in header.Keys)
                    {
                        request.AddHeader(headerKey.ToString(), header[headerKey].ToString());
                    }
                }
                if (method == Method.POST && parameter != null)
                {
                    request.AddParameter("application/json", parameter, ParameterType.RequestBody);
                }

                result = client.DownloadData(request);
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
