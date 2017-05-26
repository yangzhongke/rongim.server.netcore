using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace donet.io.rong.util
{
    class HttpUtility
    {
        public static string UrlEncode(string data)
        {
            return UrlEncoder.Default.Encode(data);
        }
    }
}
