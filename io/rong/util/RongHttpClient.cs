using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;

using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using donet.io.rong.models;
using System.Net.Http;

namespace donet.io.rong.util {
    class RongHttpClient {
    
        public static async Task<String> ExecuteGetAsync(string url) {
            if (string.IsNullOrEmpty(url)) {
                throw new ArgumentNullException("url");
            }
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            var result = await httpClient.GetAsync(url);
            if(result.StatusCode== HttpStatusCode.OK)
            {
                return await result.Content.ReadAsStringAsync();
            }
            else
            {
                return await result.Content.ReadAsStringAsync();
            }
        }


        public static async Task<String> ExecutePostAsync(String appkey, String appSecret, String methodUrl, String postStr, String contentType) {
            Random rd = new Random();
            int rd_i = rd.Next();
            String nonce = Convert.ToString(rd_i);

            String timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));

            String signature = GetHash(appSecret + nonce + timestamp);

            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            if (contentType == null || contentType.Equals("") || contentType.Length < 10)
            {
                contentType = "application/x-www-form-urlencoded";
            }

            StringContent httpContent = new StringContent(postStr,Encoding.UTF8, contentType);
            httpContent.Headers.Add("App-Key", appkey);
            httpContent.Headers.Add("Nonce", nonce);
            httpContent.Headers.Add("Timestamp", timestamp);
            httpContent.Headers.Add("Signature", signature);
                 

            var resultTask = await httpClient.PostAsync(methodUrl, httpContent);
            return await resultTask.Content.ReadAsStringAsync();
        }

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ConvertDateTimeInt(System.DateTime time) {
            System.DateTime startTime = new System.DateTime(1970, 1, 1);
            return (int)(time - startTime).TotalSeconds;
        }

        public static String GetHash(String input) {
            //建立SHA1对象
            using (SHA1 sha = SHA1.Create())
            {
                //将mystr转换成byte[]
                UTF8Encoding enc = new UTF8Encoding();
                byte[] dataToHash = enc.GetBytes(input);

                //Hash运算
                byte[] dataHashed = sha.ComputeHash(dataToHash);

                //将运算结果转换成string
                string hash = BitConverter.ToString(dataHashed).Replace("-", "");

                return hash;
            }            
        }

        /// <summary>
        /// Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error) {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None) {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }

    }
}