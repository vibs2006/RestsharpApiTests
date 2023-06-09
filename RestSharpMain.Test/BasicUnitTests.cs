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
    public class BasicUnitTests : BasicTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public BasicUnitTests(ITestOutputHelper outputHelper)
        {
            this._outputHelper = outputHelper;
        }
        [Fact]
        public async Task GetAllComponents_Tests_Unauthenticated_Returns_Valid()
        {            

            var client = new RestClient(RestClientOptions);

            var request = new RestRequest("Components/GetAllComponents");

            var response = await client.GetAsync<List<Components>>(request);

            _outputHelper.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.NotNull(response);

            response.Should().NotBeNull();

            response.FirstOrDefault().Name.Should().Be("Keys");


        }

        [Fact]
        public async Task GetAllProducts_Tests_Unauthenticated_Returns_Valid()
        {
            var client = new RestClient(RestClientOptions);

            var request = new RestRequest("Product/GetProducts");

            var response = await client.GetAsync<List<Components>>(request);

            _outputHelper.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.NotNull(response);

            response.Should().NotBeNull();

            response.FirstOrDefault().Name.Should().Be("Keys");


        }
    }
}