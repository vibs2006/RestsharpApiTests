using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpMain.Test
{
    public class BasicTests
    {
        public RestClientOptions RestClientOptions { get; set; }
        public BasicTests()
        {
                RestClientOptions = new RestClientOptions();
                RestClientOptions.BaseUrl = new Uri("https://localhost:5001/");
                RestClientOptions.RemoteCertificateValidationCallback = (_, _, _, _) => true;
        }
    }
}
