using PasswordBox.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace PasswordBox.Services
{
    class HttpAccess
    {
        /// <summary>
        /// 检查url是否合法，注意要带有http/https，可直接全选复制浏览器地址栏的地址
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static bool CheckURL(string url)
        {
            string urlReg = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";
            Regex reg = new Regex(urlReg);
            return reg.IsMatch(url);
        }

        /// <summary>
        /// 得到该网站的favicon，如果失败返回null
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>byte[]</returns>
        public static async Task<byte[]> GetIco(string url)
        {
            if (!CheckURL(url))
                return null;
            UriBuilder builder = new UriBuilder(url)
            {
                Path = "favicon.ico"
            };
            Uri target = builder.Uri;

            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            if (!headers.UserAgent.TryParseAdd(userAgent))
                return null;

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            IBuffer responseBody = null;

            try
            {
                httpResponse = await httpClient.GetAsync(target);
                httpResponse.EnsureSuccessStatusCode();
                responseBody = await httpResponse.Content.ReadAsBufferAsync();
            }
            catch (Exception)
            {
                return null;
            }

            CryptographicBuffer.CopyToByteArray(responseBody, out byte[] res);

            return res;
        }
    }
}
