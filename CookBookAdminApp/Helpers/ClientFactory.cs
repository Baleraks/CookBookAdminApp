using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Helpers
{
    internal static class ClientFactory
    {
        public static RestClient CreateClient(string url)
        {
            var optins = new RestClientOptions(url);
            optins.RemoteCertificateValidationCallback = (message, cert, chain, error) => { return true; };
            return new RestClient(optins);
        }
    }
}
