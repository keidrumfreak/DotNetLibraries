using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Net
{
    public class HttpClientProvider
    {
        private static HttpClient client;
        public static HttpClient GetClient()
        {
            return client ?? (client = new HttpClient());
        }
    }
}
