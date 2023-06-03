using RestSharp;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GraphQLProductApp.Data;
using Xunit.Abstractions;
using Newtonsoft.Json;
using NuGet.Frameworks;

namespace RestSharpMain.Test
{
    public class BasicUnitTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public BasicUnitTests(ITestOutputHelper outputHelper)
        {
            this._outputHelper = outputHelper;
        }
        [Fact]
        public async Task RestClient_InitialTest_ReturnsResponse()
        {
            var restOptions = new RestClientOptions { 
            BaseUrl = new Uri("https://localhost:5001"),
            RemoteCertificateValidationCallback = (sender,certificate,policy,errors) => true
            };

            var client = new RestClient(restOptions);

            var request = new RestRequest("Components/GetAllComponents");

            var response = await client.GetAsync<List<Components>>(request);

            _outputHelper.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.NotNull(response);

            response.Should().NotBeNull();

            response.FirstOrDefault().Name.Should().Be("Keys");


        }
    }
}