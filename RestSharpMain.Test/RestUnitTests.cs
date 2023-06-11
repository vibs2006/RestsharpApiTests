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
    public class RestUnitTests : BaseHelper
    {
        public RestUnitTests(ITestOutputHelper outputHelper) : base(outputHelper)  
        {
            _logger = outputHelper.BuildLoggerFor<RestUnitTests>();
        }

        [Fact]
        public async Task GetAllComponents_Tests_Unauthenticated_Returns_Valid()
        {

            RestClient client = RestClientInstance;
            var request = new RestRequest("Components/GetAllComponents");
            
            var response = await client.GetAsync<List<Components>>(request);

            _output.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.NotNull(response);

            response.Should().NotBeNull();

            response?.FirstOrDefault()?.Name.Should().Be("Keys");
        }
    }
}