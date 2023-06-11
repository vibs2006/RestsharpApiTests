using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace RestSharpMain.Test
{
    public static class OutputHelperUtils
    {
        public static void LogObject(this ITestOutputHelper output, Object? inputObject, Formatting formatting = Formatting.Indented)
        {
            if (inputObject is null) return;

            if (inputObject.GetType().IsClass || inputObject.GetType().IsArray)
            {
                output.WriteLine(JsonConvert.SerializeObject(inputObject, formatting)); 
            }
            else if (inputObject is string)
            {
                output.WriteLine(inputObject.ToString());
            }
        }

    }


    public class BaseHelper
    {
        public enum RestAuthenticationScheme { Bearer, Basic }
        public RestClientOptions RestClientOptions { get; set; }
        public RestClient RestClientInstance { get; set; }

        protected ITestOutputHelper _output;

        protected ILogger? _logger;
        public BaseHelper(ITestOutputHelper output)
        {
                _output = output;
                RestClientOptions = new RestClientOptions();
                RestClientOptions.BaseUrl = new Uri("https://localhost:5001/");
                RestClientOptions.RemoteCertificateValidationCallback = (_, _, _, _) => true;
            RestClientInstance = new RestClient(RestClientOptions);
        }

        public static void AddOrUpdateAuthenticationTokenHeader(RestRequest _request, string? token, RestAuthenticationScheme scheme = RestAuthenticationScheme.Bearer)
        {
            switch (scheme)
            {
                case RestAuthenticationScheme.Bearer:
                    if (_request is null) throw new NullReferenceException("_request");
                    _request.AddOrUpdateHeader("Authorization", $"Bearer {token}");
                    break;
                case RestAuthenticationScheme.Basic:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
